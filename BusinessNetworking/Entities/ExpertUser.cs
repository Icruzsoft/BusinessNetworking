﻿namespace BusinessNetworking.Entities
{
    public class ExpertUser : User
    {
        int ExpertUserId { get; set; }
        public string Address {  get; set; }
        public string City { get; set; }
        public string Country { get; set; }        
    }
}