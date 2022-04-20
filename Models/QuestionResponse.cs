using System.ComponentModel.DataAnnotations;

namespace GlobomanticsSurveys.Models
{
    public class QuestionResponse
    {
        public int Id { get; set; }
        public int SurveyResponseId { get; set; }
        public int QuestionIndex { get; set; }
        [Display(Name="Question")]
        public string QuestionText { get; set; } = string.Empty;
        [Display(Name="Response")]
        public string ResponseText { get; set; } = string.Empty;
    }
}