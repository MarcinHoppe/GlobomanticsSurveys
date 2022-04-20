using GlobomanticsSurveys.Data;
using GlobomanticsSurveys.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GlobomanticsSurveys.Controllers
{
    public class SurveyController : Controller
    {
        private readonly SurveysContext context;

        public SurveyController(SurveysContext context) => this.context = context;

        public async Task<IActionResult> Index(int id)
        {
            var survey = await context.Surveys.FindAsync(id);
            if (survey == null)
            {
                return NotFound();
            }
            return View(survey);
        }

        [HttpPost]
        public async Task<IActionResult> Start(int id)
        {
            var survey = await context
                .Surveys
                .Include(s => s.Questions)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (survey == null)
            {
                return NotFound();
            }
            var username = HttpContext.Session.GetString("User");
            if (username == null)
            {
                return Unauthorized();
            }
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                return BadRequest();
            }
            var surveyResponse = new SurveyResponse
            {
                SurveyId = survey.Id,
                UserId = user.Id,
                QuestionCount = survey.Questions.Count
            };
            context.Responses.Add(surveyResponse);
            await context.SaveChangesAsync();
            return RedirectToAction("Question", new { id = surveyResponse.Id, qno = 0 });
        }

        public async Task<IActionResult> Question(int id, int qno)
        {
            var surveyResponse = await context.Responses.FindAsync(id);
            if (surveyResponse == null)
            {
                return NotFound();
            }
            var survey = await context
                .Surveys
                .Include(s => s.Questions)
                .FirstOrDefaultAsync(s => s.Id == surveyResponse.SurveyId);
            if (survey == null)
            {
                return BadRequest();
            }
            var model = new QuestionResponse
            {
                SurveyResponseId = surveyResponse.Id,
                QuestionIndex = qno,
                QuestionText = survey.Questions[qno].Text
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RecordResponse(QuestionResponse model)
        {
            var surveyResponse = await context
                .Responses
                .Include(r => r.Responses)
                .FirstOrDefaultAsync(r => r.Id == model.SurveyResponseId);
            if (surveyResponse == null)
            {
                return NotFound();
            }

            var moreQuestions = model.QuestionIndex < surveyResponse.QuestionCount - 1;

            if (!moreQuestions)
            {
                surveyResponse.Completed = true;
            }
            surveyResponse.Responses.Add(model);
            context.Attach(surveyResponse).State = EntityState.Modified;
            await context.SaveChangesAsync();
            
            if (moreQuestions)
            {
                var routeParams = new
                {
                    id = surveyResponse.Id,
                    qno = model.QuestionIndex + 1
                };
                return RedirectToAction("Question", routeParams);
            }
            else
            {
                return RedirectToAction("Complete");
            }
        }

        public IActionResult Complete()
        {
            return View();
        }
    }
}