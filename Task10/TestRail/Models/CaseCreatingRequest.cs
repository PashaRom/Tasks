using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace Task10.TestRail.Models
{
    public class CaseCreatingRequest
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("template_id")]
        public int? TemplateId { get; set; }
        [JsonPropertyName("type_id")]
        public int? TypeId { get; set; }
        [JsonPropertyName("priority_id")]
        public int? PriorityId { get; set; }
        [JsonPropertyName("custom_steps_separated")]
        public List<Step> CustomStepsSeparated { get; set; }

    }
}
