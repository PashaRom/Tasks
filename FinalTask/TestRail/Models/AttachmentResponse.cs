using System.Text.Json.Serialization;
namespace TestRail.Models
{
    public class AttachmentResponse
    {
        [JsonPropertyName("attachment_id")]
        public int? AttachmentId { get; set; }
    }
}
