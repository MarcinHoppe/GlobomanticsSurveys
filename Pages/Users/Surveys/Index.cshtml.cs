using GlobomanticsSurveys.Data;
using GlobomanticsSurveys.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GlobomanticsSurveys.Pages.Users.Surveys
{
    public class IndexModel : PageModel
    {
        private readonly SurveysContext context;

        public IndexModel(SurveysContext context) => this.context = context;

        [BindProperty]
        public List<Survey>? AvailableSurveys { get; set; }

        public List<SurveyResponse>? UserResponses { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var username = HttpContext.Session.GetString("User");
            if (username == null)
            {
                return Unauthorized();
            }
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                HttpContext.Session.Clear();
                return BadRequest();
            }
            AvailableSurveys = await context.Surveys.ToListAsync();
            UserResponses = await context
                .Responses
                .Where(r => r.UserId == user.Id)
                .ToListAsync();
            return Page();
        }
    }
}