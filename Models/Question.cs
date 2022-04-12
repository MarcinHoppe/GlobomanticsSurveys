using System.Text.Json.Serialization;

namespace GlobomanticsSurveys.Models
{
    public class Question
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Text { get; set; } = String.Empty;

        [JsonIgnore]
        public int SurveyId { get; set; }
    }
}
