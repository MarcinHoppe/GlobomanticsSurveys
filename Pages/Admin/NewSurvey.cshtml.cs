using GlobomanticsSurveys.Data;
using GlobomanticsSurveys.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GlobomanticsSurveys.Pages.Admin
{
    public class NewSurveyModel : PageModel
    {
        private readonly SurveysContext context;

        public NewSurveyModel(SurveysContext context) => this.context = context;

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Survey Survey { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            context.Surveys.Add(Survey);
            await context.SaveChangesAsync();

            return RedirectToPage("./Surveys/View", new { id = Survey.Id });
        }
    }
}
