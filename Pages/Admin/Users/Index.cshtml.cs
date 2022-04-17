using GlobomanticsSurveys.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GlobomanticsSurveys.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        private readonly SurveysContext context;

        public IndexModel(SurveysContext context) => this.context = context;

        [BindProperty]
        public List<Models.User>? Users { get; set; }

        public async Task OnGetAsync()
        {
            Users = await context.Users.Where(u => !u.IsAdmin).ToListAsync();
        }
    }
}