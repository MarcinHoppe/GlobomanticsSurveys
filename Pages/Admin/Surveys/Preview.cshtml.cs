using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GlobomanticsSurveys.Pages.Admin.Surveys
{
    public class PreviewModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? PreviewUrl { get; set; }

        public IActionResult OnGet()
        {
            return Redirect(PreviewUrl!);
        }
    }    
}