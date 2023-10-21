namespace Backend_API.Models.DTO
{
    public class DirectionDTO
    {
        public Guid direction_id { get; set; }  
        public string direction_name { get; set; }
        public string faculty_name { get;set; }
        public Guid faculty_id { get; set; }
    }
}
