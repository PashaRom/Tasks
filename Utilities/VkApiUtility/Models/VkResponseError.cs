using System.Text;
using System.Text.Json.Serialization;
namespace Utilities.VkApiUtility.Models
{
    public class VkResponseError
    {
        [JsonPropertyName("error")]
        public Error Error { get; set; }

        public override string ToString()
        {
            StringBuilder vkResponseErrorStringBuilder = new StringBuilder();
            vkResponseErrorStringBuilder.Append("VkResponseError{");
            vkResponseErrorStringBuilder.Append($"{this.Error.ToString()}");
            vkResponseErrorStringBuilder.Append("}");
            return vkResponseErrorStringBuilder.ToString();
        }
    }
}
