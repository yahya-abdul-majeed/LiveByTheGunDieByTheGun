namespace Backend_API.Models
{
    public class Discipline
    {
        public void Deconstruct(
            out string name,
            out string _description,
            out string _literature,
            out Int16 _year,
            out Guid _grade_id,
            out bool _is_online,
            out string? _building,
            out string? _room
            )
        {
             name = discipline_name;
            _description = description;
            _literature = literature;
            _year = year;
            _grade_id = grade_id;
            _is_online = is_online;
            _building = building;
            _room = room;
        }
        public Guid discipline_id { get; set; } 
        public string discipline_name { get; set; }
        public string description { get; set; }
        public string literature { get;set; }
        public Int16 year { get; set; }
        public Guid grade_id { get; set; }  
        public bool is_online { get; set; }
        public string? building { get; set; }    
        public string? room { get; set; }
    }
}
