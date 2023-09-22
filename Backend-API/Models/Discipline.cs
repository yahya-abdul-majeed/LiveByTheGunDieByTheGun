using Backend_API.Models.UserModels;

namespace Backend_API.Models
{
    public class Discipline
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public string Title { get; set; } = string.Empty;  
        public string Description { get; set; }
        public IEnumerable<string> Literature { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
        public IEnumerable<Group> Groups { get; set; }  
        public Schedule Schedule { get; set; }    

    }
}
