using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace Task10.TestRail.Models
{
    public class Result
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }
        [JsonPropertyName("test_id")]
        public int? TestId { get; set; }
        [JsonPropertyName("status_id")]
        public int? StatusId { get; set; }
        [JsonPropertyName("created_by")]
        public int? CreatedBy { get; set; }
        [JsonPropertyName("created_on")]
        public int? CreatedOn { get; set; }
        [JsonPropertyName("assignedto_id")]
        public int? AssignedtoId { get; set; }
        [JsonPropertyName("comment")]
        public string Comment { get; set; }
        [JsonPropertyName("version")]
        public string Version { get; set; }
        [JsonPropertyName("elapsed")]
        public string Elapsed { get; set; }
        [JsonPropertyName("defects")]
        public string Defects { get; set; }
        [JsonPropertyName("attachment_ids")]
        public List<int?> AttachmentIds { get; set; }
    }
}
