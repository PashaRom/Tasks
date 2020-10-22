using System.Text.Json.Serialization;
namespace Task7.Tests.Models
{
    public class NumberOfBrowsers
    {
        [JsonPropertyName("browser")]
        public int Browser { get; set; }

        public override bool Equals(object obj)
        {
            NumberOfBrowsers numberOfBrowsers = obj as NumberOfBrowsers;
            if (numberOfBrowsers.Browser == this.Browser)
                return true;
            else
                return false;            
        }
    }
}
