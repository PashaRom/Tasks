using System.Text.Json.Serialization;
namespace TestRail.Models
{
    public class Section
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("project_id")]
        public int? ProjectId { get; set; }
        [JsonPropertyName("is_master")]
        public bool? IsMaster { get; set; }
        [JsonPropertyName("is_baseline")]
        public bool? IsBaseline { get; set; }
        [JsonPropertyName("is_completed")]
        public bool? IsCompleted { get; set; }
        [JsonPropertyName("completed_on")]
        public string CompletedOn { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
