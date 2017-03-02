using Newtonsoft.Json;

namespace Common.Model
{
    public abstract class DocumentEntity
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}