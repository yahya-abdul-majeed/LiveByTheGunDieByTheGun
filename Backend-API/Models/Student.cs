namespace Backend_API.Models
{
    public class Student
    {
        public Guid user_id { get; set; }
        public Guid faculty_id { get; set; }
        public Guid direction_id { get; set; }
        public Guid group_id { get; set; }
        public Guid grade_id { get; set; }  
    }
}
