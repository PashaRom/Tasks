using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace Task10.TestRail.Models
{
    public class Case
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("section_id")]
        public int? SectionId { get; set; }
        [JsonPropertyName("template_id")]
        public int? TemplateId { get; set; }
        [JsonPropertyName("type_id")]
        public int? TypeId { get; set; }
        [JsonPropertyName("priority_id")]
        public int? PriorityId { get; set; }
        [JsonPropertyName("milestone_id")]
        public int? MilestoneId { get; set; }
        [JsonPropertyName("refs")]
        public string Refs { get; set; }
        [JsonPropertyName("created_by")]
        public int? CreatedBy { get; set; }
        [JsonPropertyName("created_on")]
        public int? CreatedOn { get; set; }
        [JsonPropertyName("updated_by")]
        public int? UpdatedBy { get; set; }
        [JsonPropertyName("updated_on")]
        public int? UpdatedOn { get; set; }
        [JsonPropertyName("estimate")]
        public string Estimate { get; set; }
        [JsonPropertyName("estimate_forecast")]
        public string EstimateForecast { get; set; }
        [JsonPropertyName("suite_id")]
        public int? SuiteId { get; set; }
        [JsonPropertyName("display_order")]
        public int? DisplayOrder { get; set; } 
        [JsonPropertyName("custom_steps_separated")]
        public List<Step> CustomStepsSeparated { get; set; }
    }
}
