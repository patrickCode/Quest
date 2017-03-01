namespace Domain.Questions.ValueObject
{
    public class QuestionType
    {
        public static QuestionType Text = new QuestionType(1, "TXT", "Text Based");
        public static QuestionType ImageBased = new QuestionType(1, "IMG", "Image Based");
        public static QuestionType AudioBased = new QuestionType(1, "AUD", "Audio Based");

        public int Id { get; private set; }
        public string Code { get; private set; }
        public string Type { get; private set; }
        private QuestionType(int id, string code, string type)
        {
            Id = id;
            Code = code;
            Type = type;
        }

        public override string ToString()
        {
            return Type;
        }
    }
}