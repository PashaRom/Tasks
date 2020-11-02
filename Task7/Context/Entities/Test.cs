using System;
namespace Task7.Context.Entities
{
    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? StatusId { get; set; }
        public string MethodName { get; set; }
        public int? AuthorId { get; set; }
        public int ProjectId { get; set; }
        public int SessionId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Environment { get; set; }
        public string Browser { get; set; }
        public Project Project { get; set; }
    }
}
