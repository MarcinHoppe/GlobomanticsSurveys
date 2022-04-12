using GlobomanticsSurveys.Data;
using GlobomanticsSurveys.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GlobomanticsSurveys.Pages.Admin.Surveys
{
    public class DeleteModel : PageModel
    {
        private readonly SurveysContext context;
        
        public DeleteModel(SurveysContext context) => this.context = context;

        [BindProperty]
        public Survey? Survey { get; set; }

        public async Task OnGetAsync(int id)
        {
            Survey = await context.Surveys.FindAsync(id);
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var survey = await context.Surveys.Include(s => s.Questions).FirstOrDefaultAsync(s => s.Id == id);
            if (survey == null)
            {
                return NotFound();
            }
            context.Surveys.Remove(survey);
            await context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}