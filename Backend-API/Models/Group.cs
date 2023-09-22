namespace Backend_API.Models
{
    public class Group
    {
        public Guid Id { get; set; }
        public int Name { get; set; }
        public IEnumerable<Discipline> Disciplines { get; set; } = Enumerable.Empty<Discipline>();
    }
}
