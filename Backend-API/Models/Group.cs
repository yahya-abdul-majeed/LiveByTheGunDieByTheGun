namespace Backend_API.Models
{
    public class Group
    {
        public void Deconstruct(out Guid _direction_id,out string _group_name)
        {
            _direction_id = direction_id;
            _group_name = group_name;
        }
        public Guid group_id { get; set; }  
        public Guid direction_id { get; set; }  
        public string group_name { get; set; }
    }
}
