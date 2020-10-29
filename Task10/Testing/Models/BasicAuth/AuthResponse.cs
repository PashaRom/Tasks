using System.Text.Json.Serialization;
namespace Task10.Testing.Models.BasicAuth
{
    public class AuthResponse
    {
        [JsonPropertyName("authenticated")]
        public bool? Authenticated { get; set; }
        [JsonPropertyName("user")]
        public string User { get; set; }
    }
}
