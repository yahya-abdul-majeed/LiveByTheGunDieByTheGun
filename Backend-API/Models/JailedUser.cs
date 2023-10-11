
namespace Backend_API.Models
{
    public class JailedUser
    {
        public void Deconstruct(
            out string name,
            out DateTime _birthdate,
            out string _email,
            out string _phone,
            out string _avatar,
            out bool _is_student,
            out Guid _faculty_id,
            out Guid _direction_id,
            out Guid _group_id,
            out Guid _grade_id

            )
        {
            name = username;
            _birthdate = birthdate;
            _email = email;
            _phone = phone;
            _avatar = avatar;
            _is_student = is_student;
            _faculty_id = faculty_id;
            _direction_id = direction_id;
            _group_id = group_id;
            _grade_id = grade_id;


        }
        public Guid user_id { get; set; } 
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
