namespace Backend_API.Models.DTO
{
    public class EditProfileDTO
    {
        public void Deconstruct(
            out string _username,
            out string _phone,
            out DateTime _birthdate
            )
        {
            _username = username;
            _phone = phone;
            _birthdate = birthdate;
        }
        public string username { get; set; }
        public string phone {  get; set; }
        public DateTime birthdate { get; set; }
    }
}
