namespace Backend_API.Models
{
    public class Schedule
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Day { get; set; }
        public int Class { get; set; }
        public bool isOnline { get; set; }
        public string? Location { get; set; }

    }
}
