namespace Domain.Questions.ValueObject
{
    public class Answer
    {
        private readonly AnswerType _answerType;
        public string Value;
        public AnswerOption AnswerOption;

        public Answer(AnswerOption answerOption)
        {
            this.AnswerOption = answerOption;
            Value = answerOption.Value;
            _answerType = AnswerType.MCQ;
        }

        public Answer(string answer)
        {
            Value = answer;
            AnswerOption = null;
            _answerType = AnswerType.Subjective;
        }
    }
}