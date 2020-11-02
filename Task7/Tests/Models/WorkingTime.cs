using System.Text.Json.Serialization;
namespace Task7.Tests.Models
{
    public class WorkingTime
    {
        [JsonPropertyName("project")]
        public string Project { get; set; }
        [JsonPropertyName("test")]
        public string Test { get; set; }
        [JsonPropertyName("minimumWorkingTime")]
        public string MinimumWorkingTime { get; set; }

        public override bool Equals(object obj)
        {
            WorkingTime workingTime = obj as WorkingTime;
            if (workingTime.Project.Equals(this.Project)
                && workingTime.Test.Equals(this.Test)
                && workingTime.MinimumWorkingTime.Equals(this.MinimumWorkingTime))
                return true;
            else
                return false;
        }
    }
}
