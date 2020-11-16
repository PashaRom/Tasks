using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
namespace TestRail.Models
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

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("CaseCreatingRequest{");
            stringBuilder.Append($"Title={Title} ,");
            stringBuilder.Append($"TemplateId={TemplateId} ,");
            stringBuilder.Append($"TypeId={TypeId} ,");
            stringBuilder.Append($"PriorityId={PriorityId} ,");
            stringBuilder.Append($"CustomStepsSeparated[");
            CustomStepsSeparated.Select(step => stringBuilder.Append($"{step} "));
            stringBuilder.Append("]");
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
    }
}
