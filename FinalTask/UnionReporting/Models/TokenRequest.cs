using System.Text.Json.Serialization;
namespace FinalTask.UnionReporting.Models
{
    public class TokenRequest
    {
        [JsonPropertyName("variant")]
        public int Variant { get; set; }
    }
}
