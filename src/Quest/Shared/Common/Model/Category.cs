using System;
using System.Collections.Generic;

namespace Common.Model
{
    public class CategoryDto : DocumentEntity
    {
        public CategoryDto() { }
        public string Name { get; set; }
        public string Code { get; set; }
        public List<SubCategory> SubCatgories { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
    }
    public class SubCategoryDto: DocumentEntity
    {
        public SubCategoryDto() { }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
    }
}