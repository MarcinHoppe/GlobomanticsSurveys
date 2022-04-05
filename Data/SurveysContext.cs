using Microsoft.EntityFrameworkCore;

namespace GlobomanticsSurveys
{
    public class SurveysContext : DbContext
    {
        public SurveysContext(DbContextOptions<SurveysContext> options)
        : base(options)
        {
        }

        public DbSet<Survey> Surveys => Set<Survey>();
    }
}
