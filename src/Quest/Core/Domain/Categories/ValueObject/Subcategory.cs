namespace Domain.Categories.ValueObject
{
    public class Subcategory
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Audit Audit { get; set; }
    }
}