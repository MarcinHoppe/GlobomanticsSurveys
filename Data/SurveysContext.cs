using Microsoft.EntityFrameworkCore;

using GlobomanticsSurveys.Models;

namespace GlobomanticsSurveys.Data
{
    public class SurveysContext : DbContext
    {
        public SurveysContext(DbContextOptions<SurveysContext> options)
        : base(options)
        {
        }

        public DbSet<Survey> Surveys => Set<Survey>();
        public DbSet<User> Users => Set<User>();
        public DbSet<SurveyResponse> Responses => Set<SurveyResponse>();
    }
}
