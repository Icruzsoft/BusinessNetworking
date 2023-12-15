using BusinessNetworking.Entities;

namespace BusinessNetworking.Services
{

    public interface IUserService
    {
      public Task RegisterUser(User username);
    }


    public class UserService : IUserService
    {
        public UserService() { }

        public async Task RegisterUser(User username) {
             
        }
    }
}
