namespace Backend_API.Models.UserModels
{
    public class Teacher : ApplicationUser
    {
        public IEnumerable<Discipline> Disciplines { get; set; } = Enumerable.Empty<Discipline>();
    }
}
