using System.Text;
using System.Text.Json.Serialization;
namespace TestRail.Models
{
    public class Step
    {
        [JsonPropertyName("content")]
        public string Content { get; set; }
        [JsonPropertyName("expected")]
        public string Expected { get; set; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Step{");
            stringBuilder.Append($"Content={Content} ,");
            stringBuilder.Append($"Expected={Expected} ,");
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
    }
}
