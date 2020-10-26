using System.Text.Json.Serialization;
namespace Task9VK.Models
{
    public class VkResponse
    {
        [JsonPropertyName("response")]
        public Response Response { get; set; }        
    }
}
