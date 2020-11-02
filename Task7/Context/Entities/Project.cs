using System.Collections.Generic;
namespace Task7.Context.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Test> Tests { get; set; }
    }
}