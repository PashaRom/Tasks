using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace TestRail.Models
{
    public class Run
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }
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
        public bool? IncludeAll { get; set; }
        [JsonPropertyName("is_completed")]
        public bool? IsCompleted { get; set; }
        [JsonPropertyName("completed_on")]
        public int? CompletedOn { get; set; }
        [JsonPropertyName("config")]
        public string Config { get; set; }
        [JsonPropertyName("config_ids")]
        public List<int> ConfigIds { get; set; }
        [JsonPropertyName("passed_count")]
        public int? PassedCount { get; set; }
        [JsonPropertyName("blocked_count")]
        public int? BlockedCount { get; set; }
        [JsonPropertyName("untested_count")]
        public int? UntestedCount { get; set; }
        [JsonPropertyName("retest_count")]
        public int? RetestCount { get; set; }
        [JsonPropertyName("failed_count")]
        public int? FailedCount { get; set; }
        [JsonPropertyName("custom_status1_count")]
        public int? CustomStatus1Count { get; set; }
        [JsonPropertyName("custom_status2_count")]
        public int? CustomStatus2Count { get; set; }
        [JsonPropertyName("custom_status3_count")]
        public int? CustomStatus3Count { get; set; }
        [JsonPropertyName("custom_status4_count")]
        public int? CustomStatus4Count { get; set; }
        [JsonPropertyName("custom_status5_count")]
        public int? CustomStatus5Count { get; set; }
        [JsonPropertyName("custom_status6_count")]
        public int? CustomStatus6Count { get; set; }
        [JsonPropertyName("custom_status7_count")]
        public int? CustomStatus7Count { get; set; }
        [JsonPropertyName("project_id")]
        public int? ProjectId { get; set; }
        [JsonPropertyName("plan_id")]
        public int? PlanId { get; set; }
        [JsonPropertyName("created_on")]
        public int? CreatedOn { get; set; }
        [JsonPropertyName("created_by")]
        public int? CreatedBy { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
