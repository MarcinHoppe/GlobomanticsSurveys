using GlobomanticsSurveys.Data;
using GlobomanticsSurveys.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GlobomanticsSurveys.Pages.Admin.Surveys
{
    public class ViewModel : PageModel
    {
        private readonly SurveysContext context;
        private readonly LinkGenerator generator;

        public ViewModel(SurveysContext context, LinkGenerator generator)
            => (this.context, this.generator) = (context, generator);

        [BindProperty]
        public Survey? Survey { get; set; }

        [BindProperty]
        public List<Question>? Questions { get; set; }

        [BindProperty]
        public string? PreviewUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Survey = await context.Surveys.Include(s => s.Questions).FirstOrDefaultAsync(s => s.Id == id);
            if (Survey == null)
            {
                return NotFound();
            }
            Questions = new List<Question>(Survey.Questions);
            PreviewUrl = generator.GetUriByAction(HttpContext, "Index", "Survey", new { id = Survey.Id });
            return Page();
        }
    }
}