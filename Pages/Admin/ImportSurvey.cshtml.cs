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

    public class FileDownloadViewModel
    {
        [Display(Name="URL")]
        public Uri? SurveyFile { get; set; }
    }

    public class ImportSurveyModel : PageModel
    {
        private readonly SurveysContext context;
        private readonly IHttpClientFactory clientFactory;

        public ImportSurveyModel(SurveysContext context, IHttpClientFactory clientFactory) 
            => (this.context, this.clientFactory) = (context, clientFactory);

        [BindProperty]
        public FileUploadViewModel? Upload { get; set; }

        [BindProperty]
        public FileDownloadViewModel? Download { get; set; }

        private async Task<IActionResult> DeserializeAndAdd(Stream stream)
        {
            var survey = JsonSerializer.Deserialize<Survey>(stream);
            if (survey == null)
            {
                return BadRequest();
            }
            context.Surveys.Add(survey);
            await context.SaveChangesAsync();
            return RedirectToPage("./Surveys/View", new { id = survey.Id });
        }

        public async Task<IActionResult> OnPostUploadAsync()
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
                return await DeserializeAndAdd(stream);
            }
        }

        public async Task<IActionResult> OnPostDownloadAsync()
        {
            if (Download == null)
            {
                return BadRequest();
            }
            var client = clientFactory.CreateClient();
            using (var stream = await client.GetStreamAsync(Download.SurveyFile))
            {
                if (stream == null)
                {
                    return BadRequest();
                }
                return await DeserializeAndAdd(stream);
            }
        }
    }
}