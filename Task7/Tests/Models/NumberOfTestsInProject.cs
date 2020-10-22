using System.Text.Json.Serialization;
namespace Task7.Tests.Models
{
    public class NumberOfTestsInProject
    {
        [JsonPropertyName("project")]
        public string Project { get; set; }
        [JsonPropertyName("testsCount")]
        public int TestsCount { get; set; }
        public override bool Equals(object obj)
        {
            NumberOfTestsInProject number = obj as NumberOfTestsInProject;
            if (number.Project.Equals(this.Project)
                && number.TestsCount == this.TestsCount)
                return true;
            else
                return false;
        }
    }
}
