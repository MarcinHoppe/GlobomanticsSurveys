using GlobomanticsSurveys.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GlobomanticsSurveys.Pages.Admin.Users
{
    public class EditModel : PageModel
    {
        private readonly SurveysContext context;

        public EditModel(SurveysContext context) => this.context = context;

        [BindProperty]
        public Models.User? EditUser { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            EditUser = await context.Users.FindAsync(id);
            if (EditUser == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            context.Attach(EditUser!).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}