using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using GlobomanticsSurveys.Data;
using GlobomanticsSurveys.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GlobomanticsSurveys.Pages.Admin
{
    public class FileUploadViewModel
    {
        [Display(Name="File")]
        public IFormFile? SurveyFile { get; set; }
    }

    public class ImportSurveyModel : PageModel
    {
        private readonly SurveysContext context;

        public ImportSurveyModel(SurveysContext context) => this.context = context;

        [BindProperty]
        public FileUploadViewModel? Upload { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Upload == null)
            {
                return BadRequest();
            }
            using (var stream = Upload?.SurveyFile?.OpenReadStream())
            {
                if (stream == null)
                {
                    return BadRequest();
                }
                var survey = JsonSerializer.Deserialize<Survey>(stream);
                if (survey == null)
                {
                    return BadRequest();
                }
                context.Surveys.Add(survey);
                await context.SaveChangesAsync();
                return RedirectToPage("./Surveys/View", new { id = survey.Id });
            }
        }
    }
}