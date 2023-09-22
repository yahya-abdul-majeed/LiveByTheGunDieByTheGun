namespace Backend_API.Models.DTO
{
    public class RegistrationDTO
    {
        public string Name { get; set; }    
        public string Email { get; set; }
        public string Phone { get; set; }   
        public DateOnly BirthDate { get;set; }
        public bool isStudent { get; set; } 
        public int? Grade { get; set; } 
        public string FacultyID { get; set; }
        public string DirectionID { get; set; }
        public string GroupID { get; set; } 
    }
}
