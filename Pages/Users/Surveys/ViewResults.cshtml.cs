using GlobomanticsSurveys.Data;
using GlobomanticsSurveys.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GlobomanticsSurveys.Pages.Users.Surveys
{
    public class ViewResultsModel : PageModel
    {
        private readonly SurveysContext context;

        public ViewResultsModel(SurveysContext context) => this.context = context;

        [BindProperty]
        public SurveyResponse? SurveyResponse { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            SurveyResponse = await context
                .Responses
                .Include(r => r.Responses)
                .FirstOrDefaultAsync(r => r.Id == id);
            if (SurveyResponse == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}