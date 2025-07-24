using SignLogIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignLogIn.Services
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserAsync(int id);
        Task<int> SaveUserAsync(User user);
        Task<int> DeleteUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByPhoneAsync(string phone);
        Task<bool> UserExistsAsync(string email, string phone);
       
        Task<bool> IsUserExistsAsync(string email, string phone);
        Task<bool> IsValidUserAsync(string email, string password);
        

        //אולי יש עוד פעולות
    }
}
