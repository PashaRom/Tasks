using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
namespace Utilities.VkApiUtility.Models
{
    public class Error
    {
        [JsonPropertyName("error_code")]
        public int? ErrorCode { get; set; }
        [JsonPropertyName("error_msg")]
        public string ErrorMsg { get; set; }
        [JsonPropertyName("request_params")]
        public List<ResponseParam> Errors { get; set; }// = new List<ResponseParam>();

        public override string ToString()
        {
            StringBuilder errorStringBuilder = new StringBuilder();
            errorStringBuilder.Append("Error{");
            errorStringBuilder.Append($"ErrorCode=\"{this.ErrorCode}\", ");
            errorStringBuilder.Append($"ErrorMessage=\"{this.ErrorMsg}\", ");
            errorStringBuilder.Append("Errors:[");
            foreach (ResponseParam responseParam in this.Errors)
                errorStringBuilder.Append($"{responseParam}");
            errorStringBuilder.Append("]");
            errorStringBuilder.Append("}");
            return errorStringBuilder.ToString();
        }
    }
}
