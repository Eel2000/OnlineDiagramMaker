namespace OnlineDiagramMaker.Shared.Models
{
#nullable disable
    public class Project
    {

        public string Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public DateTime dateTime { get; set; }
        public string DataNodes { get; set; }
        public string DataNodesConnectors { get; set; }
        public string SessionId { get; set; }

        public virtual User User { get; set; }
    }
}