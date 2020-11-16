using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
namespace TestRail.Models
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

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("RunCreatingRequest{");
            stringBuilder.Append($"SuiteId={SuiteId}, ");
            stringBuilder.Append($"Name={Name}, ");
            stringBuilder.Append($"Description={Description}, ");
            stringBuilder.Append($"MilestoneId={MilestoneId}, ");
            stringBuilder.Append($"AssignedtoId={AssignedtoId}, ");
            stringBuilder.Append($"IncludeAll={IncludeAll}, ");
            stringBuilder.Append($"Refs={Refs}, ");
            stringBuilder.Append("CaseIds[");
            CaseIds.ForEach(id => stringBuilder.Append($"Id={id},"));
            stringBuilder.Append("]");
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
    }
}
