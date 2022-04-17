using GlobomanticsSurveys.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GlobomanticsSurveys.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SurveysContext context;

        public LoginModel(SurveysContext context) => this.context = context;

        [BindProperty]
        public string? Username { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == Username);
            if (user == null)
            {
                return Unauthorized();
            }
            HttpContext.Session.SetString("User", user.Username);
            HttpContext.Session.SetInt32("IsAdmin", user.IsAdmin ? 1 : 0);
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetLogoutAsync()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("./Index");
        }
    }
}