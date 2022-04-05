using Microsoft.EntityFrameworkCore;

namespace GlobomanticsSurveys
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
                        Title = "Customer satisfaction"
                    },
                    new Survey
                    {
                        Title = "Marketing study"
                    },
                    new Survey
                    {
                        Title = "Best movies"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}