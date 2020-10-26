using System.Text.Json.Serialization;
namespace Task9VK.Models
{
    public class VkResponseInt
    {
        [JsonPropertyName("response")]
        public int? Response { get; set; }
    }
}
