namespace Domain.Questions.ValueObject
{
    public class QuestionType
    {
        public static readonly QuestionType Text = new QuestionType(1, "TXT", "Text Based");
        public static readonly QuestionType ImageBased = new QuestionType(2, "IMG", "Image Based");
        public static readonly QuestionType AudioBased = new QuestionType(3, "AUD", "Audio Based");
        public static readonly QuestionType VideoBased = new QuestionType(4, "VID", "Video Based");

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

        public static QuestionType Get(string code)
        {
            switch(code)
            {
                case "TXT": return Text;
                case "IMG": return ImageBased;
                case "AUD": return AudioBased;
                case "VID": return VideoBased;
                default: return null;
            }
        }
    }
}