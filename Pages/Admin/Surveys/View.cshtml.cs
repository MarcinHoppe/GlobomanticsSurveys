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

        public ViewModel(SurveysContext context) => this.context = context;

        [BindProperty]
        public Survey? Survey { get; set; }

        [BindProperty]
        public List<Question>? Questions { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Survey = await context.Surveys.Include(s => s.Questions).FirstOrDefaultAsync(s => s.Id == id);
            if (Survey == null)
            {
                return NotFound();
            }
            Questions = new List<Question>(Survey.Questions);
            return Page();
        }
    }
}