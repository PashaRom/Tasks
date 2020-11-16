using System.Text.Json.Serialization;
namespace TestRail.Models
{
    public class Status
    {
        [JsonPropertyName("color_bright")]
        public int? ColorBright { get; set; }
        [JsonPropertyName("color_dark")]
        public int? ColorDark { get; set; }
        [JsonPropertyName("color_medium")]
        public int? ColorMedium { get; set; }
        [JsonPropertyName("id")]
        public int? Id { get; set; }
        [JsonPropertyName("is_final")]
        public bool? IsFinal { get; set; }
        [JsonPropertyName("is_system")]
        public bool? IsSystem { get; set; }
        [JsonPropertyName("is_untested")]
        public bool? IsUntested { get; set; }
        [JsonPropertyName("label")]
        public string Label { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
