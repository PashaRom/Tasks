using System.Text.Json.Serialization;
namespace TestRail.Models
{
    public class User
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }
    }
}
