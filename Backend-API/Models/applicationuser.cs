﻿namespace Backend_API.Models
{
    public class applicationuser
    {
        public Guid user_id { get; set; }   
        public string username { get; set; }
        public DateTime birthdate { get; set; } 
        public string email { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string avatar { get; set; }
    }
}
