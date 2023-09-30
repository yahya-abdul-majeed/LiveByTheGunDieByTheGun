using Backend_API.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Backend_API.Models.DTO
{
    //[ModelBinder(typeof(RegistrationModelBinder))]
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
        public int? Grade { get; set; } = null;
        public string? FacultyID { get; set; } = null;
        public string? DirectionID { get; set; } = null;
        public string? GroupID { get; set; } = null;
    }
}
