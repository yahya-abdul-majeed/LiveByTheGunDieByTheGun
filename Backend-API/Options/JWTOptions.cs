namespace Backend_API.Options
{
    public class JWTOptions
    {
        public const string JWT = "JWT";
        public string ValidAudience { get; set; }   
        public string ValidIssuer { get; set; }
        public string Secret { get; set; }
    }
}
