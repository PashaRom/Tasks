using System.Text.Json.Serialization;
namespace Task9VK.Models.ResponseModels
{
    public class Size
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("width")]
        public int? Width { get; set; }
        [JsonPropertyName("height")]
        public int? Height { get; set; }
    }
}
