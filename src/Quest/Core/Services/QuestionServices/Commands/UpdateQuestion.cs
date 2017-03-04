using Common.Domain;
using Model = Common.Model;

namespace Services.QuestionServices.Commands
{
    public class UpdateQuestion: Command
    {
        public Model.QuestionDto UpdatedQuestion { get; set; }
        public UpdateQuestion(Model.QuestionDto modelQuestion): base()
        {
            UpdatedQuestion = modelQuestion;
        }
    }
}