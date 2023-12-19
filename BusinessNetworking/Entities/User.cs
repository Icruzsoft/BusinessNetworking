using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessNetworking.Entities
{
    public class User
    {     
        public int UserId { get; set; }       
        public string UserName { get; set; }        
        public string Name { get; set; }  
        public string LastName { get; set; }    
        public string Email { get; set; }        
        public string PhoneNumber { get; set; }      
        public string Password { get; set; }     
        public bool TermsAccepted { get; set; }        
        public DateTime CreatedDate { get; set; }
        public UserType UserType { get; set; }
    }
}
