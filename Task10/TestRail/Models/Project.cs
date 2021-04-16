using System.Text.Json.Serialization;
namespace Task10.TestRail.Models
{
    public class Project
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("announcement")]
        public string Announcement { get; set; }
        [JsonPropertyName("show_announcement")]
        public bool? ShowAnnouncement { get; set; }
        [JsonPropertyName("is_completed")]
        public bool? IsCompleted { get; set; }
        [JsonPropertyName("completed_on")]
        public string CompletedOn { get; set; }
        [JsonPropertyName("SuiteMode")]
        public int? SuiteMode { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
