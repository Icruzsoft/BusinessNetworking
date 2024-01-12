using System.ComponentModel.DataAnnotations;

namespace BusinessNetworking.Entities
{
    public class UserType
    {
        public int UserTypeId { get; set; }
        public string TypeName { get; set; }
        public int IdentityIdentity { get; set; }
        public int IdentitySeed { get; set; }
        public int IdentityIncrement { get; set; }
        public List<ClientUser> Clients { get; set; }
        public List<ExpertUser> Experts { get; set; }
    }
}




