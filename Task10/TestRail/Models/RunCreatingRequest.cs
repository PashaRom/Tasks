using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace Task10.TestRail.Models
{
    public class RunCreatingRequest
    {
        [JsonPropertyName("suite_id")]
        public int? SuiteId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("milestone_id")]
        public int? MilestoneId { get; set; }
        [JsonPropertyName("assignedto_id")]
        public int? AssignedtoId { get; set; }
        [JsonPropertyName("include_all")]
        public bool IncludeAll { get; set; }
        [JsonPropertyName("case_ids")]
        public List<int?> CaseIds { get; set; }
        [JsonPropertyName("refs")]
        public string Refs { get; set; }
    }
}
