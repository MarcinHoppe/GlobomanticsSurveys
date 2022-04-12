using System.Text.Json.Serialization;

namespace GlobomanticsSurveys.Models
{
    public class Survey
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public IList<Question> Questions { get; set; } = new List<Question>();
    }
}