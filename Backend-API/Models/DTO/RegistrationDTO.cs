namespace Backend_API.Models.DTO
{
    public class RegistrationDTO
    {
        public string username { get; set; }
        public DateTime birthdate { get; set; }
        public string email { get; set; }
        public string phone { get; set; }   
        public string avatar { get; set; }
        public bool is_student { get; set; }    
        public Guid faculty_id { get; set; }    
        public Guid direction_id { get; set; }
        public Guid group_id { get; set; }
        public Guid grade_id { get; set; }  
    }
}
