namespace Backend_API.Models.UserModels
{
    public class Student : ApplicationUser
    {
        public Faculty Faculty { get; set; }
        public Direction Direction { get; set; }
        public Group Group { get; set; }
        public int Grade { get; set; }  

    }
}
