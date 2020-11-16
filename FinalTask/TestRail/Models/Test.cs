using System.Text.Json.Serialization;
namespace TestRail.Models
{
    public class Test
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }
        [JsonPropertyName("case_id")]
        public int? CaseId { get; set; }
        [JsonPropertyName("status_id")]
        public int? StatusId { get; set; }
        [JsonPropertyName("assignedto_id")]
        public int? AssignedtoId { get; set; }
        [JsonPropertyName("run_id")]
        public int? RunId { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("template_id")]
        public int? TemplateId { get; set; }
        [JsonPropertyName("type_id")]
        public int? TypeId { get; set; }
        [JsonPropertyName("priority_id")]
        public int? PriorityId { get; set; }
        [JsonPropertyName("estimate")]
        public string Estimate { get; set; }
        [JsonPropertyName("estimate_forecast")]
        public string EstimateForecast { get; set; }
        [JsonPropertyName("refs")]
        public string Refs { get; set; }
        [JsonPropertyName("milestone_id")]
        public int? MilestoneId { get; set; }               
    }
}
