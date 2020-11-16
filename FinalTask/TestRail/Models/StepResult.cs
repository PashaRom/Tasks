using System.Text;
using System.Text.Json.Serialization;
namespace TestRail.Models
{
    public class StepResult : Step
    {
        [JsonPropertyName("actual")]
        public string Actual { get; set; }
        [JsonPropertyName("status_id")]
        public int? StatusId { get; set; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("StepResult{");
            stringBuilder.Append($"Content={Content}, ");
            stringBuilder.Append($"Expected={Expected}, ");
            stringBuilder.Append($"Actual={Actual}, ");
            stringBuilder.Append($"StatusId={StatusId}");
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
    }
}
