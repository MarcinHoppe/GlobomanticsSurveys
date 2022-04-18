using GlobomanticsSurveys.Data;
using GlobomanticsSurveys.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GlobomanticsSurveys.Pages.Users.Surveys
{
    public class IndexModel : PageModel
    {
        private readonly SurveysContext context;

        public IndexModel(SurveysContext context) => this.context = context;

        [BindProperty]
        public List<Survey>? Surveys { get; set; }

        public async Task OnGetAsync()
        {
            Surveys = await context.Surveys.ToListAsync();
        }
    }
}