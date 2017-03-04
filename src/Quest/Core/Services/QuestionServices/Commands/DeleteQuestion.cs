using Common.Domain;

namespace Services.QuestionServices.Commands
{
    public class DeleteQuestion: Command
    {
        public string QuestionId { get; set; }
        public DeleteQuestion(string questionId)
        {
            QuestionId = questionId;
        }
    }
}