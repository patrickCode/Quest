using Common.Domain;
using Model = Common.Model;

namespace Services.QuestionServices.Commands
{
    public class AddQuestion: Command
    {
        public Model.QuestionDto NewQuestion { get; set; }
        public AddQuestion(Model.QuestionDto modelQuestion): base()
        {
            NewQuestion = modelQuestion;
        }
    }
}