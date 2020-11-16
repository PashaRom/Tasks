using System.Text;
using System.Text.Json.Serialization;
namespace TestRail.Models
{
    public class SectionCreatingRequest
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("suite_id")]
        public int? SuiteId { get; set; }
        [JsonPropertyName("parent_id")]
        public int? ParentId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("SectionCreatingRequest{");
            stringBuilder.Append($"Description={Description}, ");
            stringBuilder.Append($"SuiteId={SuiteId}, ");
            stringBuilder.Append($"ParentId={ParentId}, ");
            stringBuilder.Append($"Name={Name}");
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
    }
}
