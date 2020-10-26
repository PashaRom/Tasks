using System.Text.Json.Serialization;
namespace Task9VK.Models.ResponseModels
{
    public class UploadServer
    {
        [JsonPropertyName("server")]
        public int? Server { get; set; }
        [JsonPropertyName("photo")]
        public string Photo { get; set; }
        [JsonPropertyName("hash")]
        public string Hash { get; set; }       
    }
}
