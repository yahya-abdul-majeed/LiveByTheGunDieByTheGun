namespace Backend_API.Models
{
    public class Direction
    {
        public void Deconstruct(out string name, out Guid facultyid)
        {
            name = direction_name;
            facultyid = faculty_id;
        }
        public Guid direction_id { get; set; }
        public Guid faculty_id { get; set; }    
        public string direction_name { get; set;}
    }
}
