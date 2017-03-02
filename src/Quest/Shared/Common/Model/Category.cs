using System.Collections.Generic;

namespace Common.Model
{
    public class Category: DocumentEntity
    {
        public Category() { }
        public string Value { get; set; }
        public string Code { get; set; }
        public List<SubCategory> SubCatgories { get; set; }

    }
    public class SubCategory
    {
        public SubCategory() { }
        public string Value { get; set; }
        public string Code { get; set; }
    }
}