using System.Text.Json.Serialization;
namespace Task10.TestRail.Models
{
    public class SuiteCreatingRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
