
using System.Text.Json.Serialization;
namespace TestRail.Models
{
    public class ResponseError
    {
        [JsonPropertyName("error")]
        public string Error { get; set; }
    }
}
