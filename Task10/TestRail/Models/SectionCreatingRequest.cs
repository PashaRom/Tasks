using System.Text.Json.Serialization;
namespace Task10.TestRail.Models
{
    public class SectionCreatingRequest
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("suite_id")]
        public int? SuiteId { get; set; }
        [JsonPropertyName("parent_id")]
        public int? ParentId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
