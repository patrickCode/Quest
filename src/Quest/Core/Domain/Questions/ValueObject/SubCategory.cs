namespace Domain.Questions.ValueObject
{
    public class SubCategory
    {
        public string Value { get; private set; }
        public string Code { get; private set; }
        public SubCategory(string value, string code)
        {
            Value = value;
            Code = code;
        }
    }
}