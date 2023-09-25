namespace Backend_API.Models.Responses
{
    public class EmailResponse
    {
        public bool IsSuccess { get; set; }
        public List<string> ErrorMessages { get; set; } = new List<string>();
    }
}
