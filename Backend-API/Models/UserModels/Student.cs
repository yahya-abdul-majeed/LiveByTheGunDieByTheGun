namespace Backend_API.Models.UserModels
{
    public class Student : ApplicationUser
    {
        public string FacultyID { get; set; }
        public string DirectionID { get; set; }
        public string GroupID { get; set; }
        public int Grade { get; set; }  

    }
}
