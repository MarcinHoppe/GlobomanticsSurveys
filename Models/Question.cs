namespace GlobomanticsSurveys.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; } = String.Empty;

        public int SurveyId { get; set; }
    }
}
