namespace Backend_API.Models
{
    public class Faculty
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public IEnumerable<Direction> Directions { get; set; }  = Enumerable.Empty<Direction>();
    }
}
