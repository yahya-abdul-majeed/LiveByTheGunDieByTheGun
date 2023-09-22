namespace Backend_API.Models.UserModels
{
    public class Schedule
    {
        public int Day { get; set; }    
        public int Class { get; set; }  
        public bool isOnline { get; set; }
        public string? Location { get; set; }   

    }
}
