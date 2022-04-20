namespace GlobomanticsSurveys.Models
{
    public class SurveyResponse
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public int UserId { get; set; }
        public bool Completed { get; set; } = false;
        public int QuestionCount { get; set; }
        public IList<QuestionResponse> Responses { get; set; } = new List<QuestionResponse>();
    }
}