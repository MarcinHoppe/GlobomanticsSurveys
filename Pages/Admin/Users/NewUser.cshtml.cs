using GlobomanticsSurveys.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GlobomanticsSurveys.Pages.Admin.Users
{
    public class NewUserModel : PageModel
    {
        private readonly SurveysContext context;

        public NewUserModel(SurveysContext context) => this.context = context;

        [BindProperty]
        public Models.User NewUser { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            context.Users.Add(NewUser);
            await context.SaveChangesAsync();
            
            return RedirectToPage("./Index");
        }
    }
}