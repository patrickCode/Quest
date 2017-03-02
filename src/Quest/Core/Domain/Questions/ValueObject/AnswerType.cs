namespace Domain.Questions.ValueObject
{
    public class AnswerType
    {
        public static readonly AnswerType MCQ = new AnswerType(1, "MCQ", "Multi Choice Answer");
        public static readonly AnswerType Subjective = new AnswerType(1, "SUB", "Subjective");

        public int Id { get; private set; }
        public string Code { get; private set; }
        public string Type { get; private set; }
        private AnswerType(int id, string code, string type)
        {
            Id = id;
            Code = code;
            Type = type;
        }

        public override string ToString()
        {
            return Type;
        }

        public static AnswerType Get(string code)
        {
            switch (code)
            {
                case "MCQ": return MCQ;
                case "SUB": return Subjective;
                default: return null;
            }
        }
    }
}