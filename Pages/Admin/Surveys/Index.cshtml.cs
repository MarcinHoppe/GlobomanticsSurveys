using GlobomanticsSurveys.Data;
using GlobomanticsSurveys.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GlobomanticsSurveys.Pages.Admin.Surveys
{
    public class IndexModel : PageModel
    {
        private readonly SurveysContext context;

        public IndexModel(SurveysContext context) => this.context = context;

        public IList<Survey> Surveys { get; set; } = new List<Survey>();

        public async Task OnGetAsync()
        {
            Surveys = await context.Surveys.ToListAsync();
        }
    }
}