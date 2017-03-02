using System.Collections.Generic;

namespace Domain.Questions.ValueObject
{
    public class Category
    {
        public string Value { get; private set; }
        public string Code { get; private set; }
        public List<SubCategory> SubCategories { get; private set; }
        public Category(string value, string code, List<SubCategory> subCategories)
        {
            Value = value;
            Code = code;
            SubCategories = subCategories;
        }
        public Category(string value, string code)
            : this(value, code, new List<SubCategory>())
        {
        }
    }
}