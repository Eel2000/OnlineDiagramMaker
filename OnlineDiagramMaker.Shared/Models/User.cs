namespace OnlineDiagramMaker.Shared.Models
{
#nullable disable
    public class User
    {
        public User()
        {
            Projects = new HashSet<Project>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Project> Projects { get; set;}
    }
}