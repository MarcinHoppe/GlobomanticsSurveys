using System.ComponentModel.DataAnnotations;
using GlobomanticsSurveys.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GlobomanticsSurveys.Pages.Users.Profile
{
    public class AvatarUploadViewModel
    {
        [Display(Name="File")]
        public IFormFile? AvatarFile { get; set; }
    }

    public class UploadAvatarModel : PageModel
    {
        private readonly SurveysContext context;
        private readonly IWebHostEnvironment environment;

        public UploadAvatarModel(SurveysContext context, IWebHostEnvironment environment) 
            => (this.context, this.environment) = (context, environment);

        [BindProperty]
        public AvatarUploadViewModel? Upload { get; set; }

        public async Task<IActionResult> OnPost()
        {
            var username = HttpContext.Session.GetString("User");
            if (username == null)
            {
                return Unauthorized();
            }
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                HttpContext.Session.Clear();
                return BadRequest();
            }
            if (Upload == null || Upload.AvatarFile == null)
            {
                return BadRequest();
            }
            using (var stream = Upload.AvatarFile.OpenReadStream())
            {
                if (stream == null)
                {
                    return BadRequest();
                }
                var fileName = Upload.AvatarFile.FileName!;
                var destinationPath = Path.Combine(environment.WebRootPath, fileName);
                using (var outStream = System.IO.File.Create(destinationPath))
                {
                    await stream.CopyToAsync(outStream);
                }
                user.Avatar = fileName;
                await context.SaveChangesAsync();
            }
            return RedirectToPage("./Index", new { id = user.Id });
        }
    }
}