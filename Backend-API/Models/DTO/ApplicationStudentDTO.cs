namespace Backend_API.Models.DTO
{
    public class ApplicationStudentDTO
    {
        public Guid user_id { get; set; }   
        public string username { get; set; }
        public DateTime birthdate { get; set; } 
        public string email { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string avatar { get; set; }
        public Guid faculty_id { get; set; }
        public string faculty_name { get; set; }
        public Guid direction_id { get; set; }
        public string direction_name { get; set; }
        public Guid group_id { get; set; }
        public string group_name { get; set; }
        public Guid grade_id { get; set; }  
        public string grade_name { get; set; }
    }
}
