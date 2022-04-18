using GlobomanticsSurveys.Data;
using GlobomanticsSurveys.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GlobomanticsSurveys.Pages.Users.Profile
{
    public class EditProfileModel : PageModel
    {
        private readonly SurveysContext context;

        public EditProfileModel(SurveysContext context) => this.context = context;

        [BindProperty]
        public User? EditUser { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var username = HttpContext.Session.GetString("User");
            if (username == null)
            {
                return Unauthorized();
            }
            EditUser = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (EditUser == null)
            {
                HttpContext.Session.Clear();
                return BadRequest();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            context.Attach(EditUser!).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}