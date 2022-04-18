using GlobomanticsSurveys.Data;
using GlobomanticsSurveys.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GlobomanticsSurveys.Pages.Users.Profile
{
    public class IndexModel : PageModel
    {
        private readonly SurveysContext context;

        public IndexModel(SurveysContext context) => this.context = context;

        [BindProperty]
        public User? CurrentUser { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var username = HttpContext.Session.GetString("User");
            if (username == null)
            {
                return Unauthorized();
            }
            CurrentUser = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (CurrentUser == null)
            {
                HttpContext.Session.Clear();
                return BadRequest();
            }
            return Page();
        }
    }
}