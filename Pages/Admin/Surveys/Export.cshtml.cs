using System.Text;
using System.Text.Json;
using GlobomanticsSurveys.Data;
using GlobomanticsSurveys.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GlobomanticsSurveys.Pages.Admin.Surveys
{
    public class ExportModel : PageModel
    {
        private readonly SurveysContext context;

        public ExportModel(SurveysContext context) => this.context = context;

        [BindProperty]
        public Survey? Survey { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Survey = await context.Surveys.FindAsync(id);
            if (Survey == null)
            {
                return NotFound();
            }
            return Page();
        }

        private static byte[] Serialize(Survey survey)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
            };
            var serializedSurvey = JsonSerializer.Serialize(survey, options);
            return Encoding.UTF8.GetBytes(serializedSurvey);
        }

        public async Task<IActionResult> OnGetDownloadAsync(int id)
        {
            var fullSurvey = await context.Surveys.Include(s => s.Questions).FirstAsync(s => s.Id == id);
            if (fullSurvey == null)
            {
                return NotFound();
            }
            var bytes = Serialize(fullSurvey);
            return File(bytes, "application/json", "survey.json");
        }
    }
}