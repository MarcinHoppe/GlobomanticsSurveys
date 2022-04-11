using GlobomanticsSurveys.Data;
using GlobomanticsSurveys.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GlobomanticsSurveys.Pages.Admin.Surveys
{
    public class EditModel : PageModel
    {
        private readonly SurveysContext context;

        public EditModel(SurveysContext context) => this.context = context;

        [BindProperty]
        public Survey? Survey { get; set; }

        [BindProperty]
        public List<Question>? Questions { get; set; }

        [BindProperty]
        public Question? NewQuestion { get; set; }

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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            foreach (var question in Questions!)
            {
                context.Attach(question!).State = EntityState.Modified;
            }
            context.Attach(Survey!).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return RedirectToPage("./View", new { id = Survey?.Id });
        }

        public async Task<IActionResult> OnPostAddAsync(int id)
        {
            Survey = await context.Surveys.Include(s => s.Questions).FirstOrDefaultAsync(s => s.Id == id);
            if (Survey == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrWhiteSpace(NewQuestion?.Text))
            {
                Survey.Questions.Add(NewQuestion!);
            }
            context.Attach(Survey!).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return RedirectToPage("./View", new { id = Survey?.Id });
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id, int qid)
        {
            Survey = await context.Surveys.Include(s => s.Questions).FirstOrDefaultAsync(s => s.Id == id);
            if (Survey == null)
            {
                return NotFound();
            }
            var question = Survey.Questions.FirstOrDefault(q => q.Id == qid);
            if (question == null)
            {
                return NotFound();
            }
            Survey.Questions.Remove(question);
            context.Attach(Survey).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return RedirectToPage("./View", new { id = Survey?.Id });
        }
    }
}