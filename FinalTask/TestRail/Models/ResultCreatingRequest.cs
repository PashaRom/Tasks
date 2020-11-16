using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
namespace TestRail.Models
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
        [JsonPropertyName("custom_step_results")]
        public List<StepResult> CustomStepResults { get; set; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("ResultCreatingRequest{");
            stringBuilder.Append($"StatusId={StatusId}, ");
            stringBuilder.Append($"Comment={Comment}, ");
            stringBuilder.Append($"Version={Version}, ");
            stringBuilder.Append($"Elapsed={Elapsed}, ");
            stringBuilder.Append($"Defects={Defects}, ");
            stringBuilder.Append($"AssignedtoId={AssignedtoId}, ");
            stringBuilder.Append("CustomStepResults[");
            CustomStepResults.ForEach(stepResult => stringBuilder.Append($"StepResult={stepResult}, "));
            stringBuilder.Append("]");
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
    }
}
