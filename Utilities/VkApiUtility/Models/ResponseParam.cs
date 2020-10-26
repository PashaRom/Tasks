using System.Text;
using System.Text.Json.Serialization;
namespace Utilities.VkApiUtility.Models
{
    public class ResponseParam
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }
        [JsonPropertyName("value")]
        public string Value { get; set; }

        public override string ToString()
        {
            StringBuilder responseParamStringBuilder = new StringBuilder();
            responseParamStringBuilder.Append("ResponseParam{");
            responseParamStringBuilder.Append($"Key=\"{this.Key}\", ");
            responseParamStringBuilder.Append($"Value=\"{this.Value}\"");
            responseParamStringBuilder.Append("}");
            return responseParamStringBuilder.ToString();
        }
    }
}
