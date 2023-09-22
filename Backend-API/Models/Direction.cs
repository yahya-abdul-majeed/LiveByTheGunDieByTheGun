namespace Backend_API.Models
{
    public class Direction
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Group> groups { get; set; }
        public Faculty Faculty { get; set; }
    }
}
