using System.Text.Json.Serialization;
namespace Task10.TestRail.Models
{
    public class AttachmentResponse
    {
        [JsonPropertyName("attachment_id")]
        public int? AttachmentId { get; set; }
    }
}
