using System.Text.Json.Serialization;
namespace Task10.TestRail.Models
{
    public class ResultCreatingRequest
    {
        [JsonPropertyName("status_id")]
        public int? StatusId { get; set; }
        [JsonPropertyName("comment")]
        public string Comment { get; set; }
        [JsonPropertyName("version")]
        public string Version { get; set; }
        [JsonPropertyName("elapsed")]
        public string Elapsed { get; set; }
        [JsonPropertyName("defects")]
        public string Defects { get; set; }
        [JsonPropertyName("assignedto_id")]
        public int? AssignedtoId { get; set; }
    }
}
