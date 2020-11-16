using System;
using System.Text;
using System.Text.Json.Serialization;
namespace FinalTask.Tests.Models
{
    public class UIAddedTest : Test
    {
        public string Environment { get; set; }
        public string Browser { get; set; }
        [JsonPropertyName("log")]
        public string Log { get; set; }

        public override bool Equals(object obj)
        {
            UIAddedTest uiAddedTest = obj as UIAddedTest;
            return (uiAddedTest.Name ?? String.Empty).Trim().ToLower().Equals((Name ?? String.Empty).Trim().ToLower())
                && (uiAddedTest.Method ?? String.Empty).Trim().ToLower().Equals((Method ?? String.Empty).Trim().ToLower())
                && (uiAddedTest.Status ?? String.Empty).Trim().ToLower().Equals((Status ?? String.Empty).Trim().ToLower())
                && (uiAddedTest.Environment ?? String.Empty).Trim().ToLower().Equals((Environment ?? String.Empty).Trim().ToLower())
                && (uiAddedTest.Browser ?? String.Empty).Trim().ToLower().Equals((Browser ?? String.Empty).Trim().ToLower())
                && (uiAddedTest.StartTime ?? String.Empty).Trim().ToLower().Equals((StartTime ?? String.Empty).Trim().ToLower())
                && (uiAddedTest.EndTime ?? String.Empty).Trim().ToLower().Equals((EndTime ?? String.Empty).Trim().ToLower())
                && (uiAddedTest.Log ?? String.Empty).Trim().ToLower().Equals((Log ?? String.Empty).Trim().ToLower());
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("UIAddedTest{");
            stringBuilder.Append(String.Format("Name={0}, ", Name ?? String.Empty));
            stringBuilder.Append(String.Format("Method={0}, ", Method ?? String.Empty));
            stringBuilder.Append(String.Format("Status={0}, ", Status ?? String.Empty));
            stringBuilder.Append(String.Format("StartTime={0}, ", StartTime ?? String.Empty));
            stringBuilder.Append(String.Format("EndTime={0}, ", EndTime ?? String.Empty));
            stringBuilder.Append(String.Format("Environment={0}, ", Environment ?? String.Empty));
            stringBuilder.Append(String.Format("Browser={0}, ", Browser ?? String.Empty));
            stringBuilder.Append(String.Format("Log={0}", Log ?? String.Empty));
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
    }
}
