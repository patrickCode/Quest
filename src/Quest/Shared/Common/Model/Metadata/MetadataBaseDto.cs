namespace Common.Model.Metadata
{
    public class MetadataBaseDto: DocumentEntity
    {
        public string MetadataType { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}