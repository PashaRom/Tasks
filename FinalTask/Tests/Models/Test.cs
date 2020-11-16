using System;
using System.Text;
using System.Xml.Serialization;
using System.Text.Json.Serialization;
namespace FinalTask.Tests.Models
{
    [XmlType("test")]
    public class Test
    {
        [XmlElement("duration")]
        public string Duration { get; set; }
        [JsonPropertyName("method")]
        [XmlElement("method")]
        public string Method { get; set; }
        [JsonPropertyName("name")]
        [XmlElement("name")]
        public string Name { get; set; }        
        [XmlElement("startTime")]
        public string StartTime { get; set; }
        [XmlElement("endTime")]
        public string EndTime { get; set; }
        [JsonPropertyName("status")]
        [XmlElement("status")]
        public string Status { get; set; }

        public override bool Equals(object obj)
        {
            Test test = obj as Test;
            return Duration.ToLower().Trim().Equals(test.Duration.ToLower().Trim())
                && Method.ToLower().Trim().Equals(test.Method.ToLower().Trim())
                && Name.ToLower().Trim().Equals(test.Name.ToLower().Trim())
                && StartTime.ToLower().Trim().Equals(test.StartTime.ToLower().Trim())
                && Status.ToLower().Trim().Equals(test.Status.ToLower().Trim())
                && (EndTime ?? String.Empty).ToLower().Trim().Equals((test.EndTime ?? String.Empty).ToLower().Trim());               
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Test{");
            stringBuilder.Append(String.Format("Name={0}, ", Name ?? String.Empty));
            stringBuilder.Append(String.Format("Method={0}, ", Method ?? String.Empty));
            stringBuilder.Append(String.Format("Status={0}, ", Status ?? String.Empty));
            stringBuilder.Append(String.Format("StartTime={0}, ", StartTime ?? String.Empty));
            stringBuilder.Append(String.Format("EndTime={0}", EndTime ?? String.Empty));
            stringBuilder.Append(String.Format("Duration={0}", Duration ?? String.Empty));
            stringBuilder.Append("}");            
            return stringBuilder.ToString();
        }
    }
}
