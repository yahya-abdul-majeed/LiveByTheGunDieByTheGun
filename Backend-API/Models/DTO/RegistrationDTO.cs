using System.ComponentModel.DataAnnotations;

namespace Backend_API.Models.DTO
{
    public class RegistrationDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }   
        [Required]
        public DateOnly BirthDate { get;set; }
        [Required]
        public bool isStudent { get; set; } 
        public int? Grade { get; set; } 
        public string FacultyID { get; set; }
        public string DirectionID { get; set; }
        public string GroupID { get; set; } 
    }
}
