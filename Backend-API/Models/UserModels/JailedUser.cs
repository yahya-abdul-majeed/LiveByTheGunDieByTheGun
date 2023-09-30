namespace Backend_API.Models.UserModels
{
    public class JailedUser
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }   
        public DateOnly BirthDate { get; set; }
        public string Avatar { get; set; } = string.Empty;
        public string? FacultyID { get; set; } = null;
        public string? DirectionID { get; set; } = null;
        public string? GroupID { get; set; } = null;
        public int? Grade { get; set; } = null;  
    }
}
