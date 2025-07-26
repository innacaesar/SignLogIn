using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignLogIn.Services
{
    public interface IAuthService
    {
        bool IsLoggedIn { get; }
        event Action AuthStatusChanged;

        Task<bool> LoginAsync(string email, string password);
        void Logout();
    }
}
