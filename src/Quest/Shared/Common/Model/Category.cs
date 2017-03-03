using System.Collections.Generic;

namespace Common.Model
{
    public class CategoryDto: DocumentEntity
    {
        public CategoryDto() { }
        public string Value { get; set; }
        public string Code { get; set; }
        public List<SubCategoryDto> SubCatgories { get; set; }

    }
    public class SubCategoryDto
    {
        public SubCategoryDto() { }
        public string Value { get; set; }
        public string Code { get; set; }
    }
}