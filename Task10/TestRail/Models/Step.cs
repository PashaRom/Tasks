using System.Text.Json.Serialization;
namespace Task10.TestRail.Models
{
    public class Step
    {
        [JsonPropertyName("content")]
        public string Content { get; set; }
        [JsonPropertyName("expected")]
        public string Expected { get; set; }
    }
}
