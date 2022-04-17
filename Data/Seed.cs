using Microsoft.EntityFrameworkCore;

using GlobomanticsSurveys.Models;

namespace GlobomanticsSurveys.Data
{
    public static class Seed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var options = serviceProvider.GetRequiredService<DbContextOptions<SurveysContext>>();
            using (var context = new SurveysContext(options))
            {
                var anyData = context.Surveys?.Any<Survey>() ?? false;
                if (anyData)
                {
                    return;
                }

                context.AddRange(
                    new Survey
                    {
                        Title = "Customer satisfaction",
                        Questions = new List<Question>
                        {
                            new() { Text = "Lorem ipsum dolor sit amet" },
                            new() { Text = "consectetur adipiscing elit" },
                            new() { Text = "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua" }
                        }
                    },
                    new Survey
                    {
                        Title = "Marketing study",
                        Questions = new List<Question>
                        {
                            new() { Text = "Ut enim ad minim veniam" },
                            new() { Text = "quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat" },
                        }
                    },
                    new Survey
                    {
                        Title = "Best movies",
                        Questions = new List<Question>
                        {
                            new() { Text = "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur" },
                            new() { Text = "Excepteur sint occaecat cupidatat non proident" },
                            new() { Text = "sunt in culpa qui officia deserunt mollit anim id est laborum" },
                        }
                    }
                );

                context.AddRange(
                    new User
                    {
                        Username = "Jane"
                    },
                    new User
                    {
                        Username = "Joe"
                    },
                    new User
                    {
                        Username = "admin",
                        IsAdmin = true
                    }
                );

                context.SaveChanges();
            }
        }
    }
}