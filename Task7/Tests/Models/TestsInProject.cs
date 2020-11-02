using System;
using System.Text.Json.Serialization;
namespace Task7.Tests.Models
{
    public class TestsInProject
    {
        [JsonPropertyName("project")]
        public string Project { get; set; }
        [JsonPropertyName("test")]
        public string Test { get; set; }
        [JsonPropertyName("date")]
        public DateTime? Date { get; set; }

        public override bool Equals(object obj)
        {
            TestsInProject testsInProject = obj as TestsInProject;
            if (testsInProject.Project.Equals(this.Project)
                && testsInProject.Test.Equals(this.Test)
                && testsInProject.Date.Value.Date.Equals(this.Date.Value.Date))
                return true;
            else
                return false;
        }
    }
}
