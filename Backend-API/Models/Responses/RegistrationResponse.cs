namespace Backend_API.Models.Responses
{
    public class RegistrationResponse
    {
        public bool IsSuccess { get; set; } 
        public List<string> ErrorMessages { get; set; }
    }
}
