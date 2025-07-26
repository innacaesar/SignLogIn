using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignLogIn.Services
{
    public class AuthService : IAuthService
    {
        public bool IsLoggedIn { get; private set; }

        public event Action AuthStatusChanged;

        public async Task<bool> LoginAsync(string email, string password)
        {
            await Task.Delay(100); // Simulate network delay
            // Here you would typically call your authentication API
            // For this example, let's assume the login is always successful
            IsLoggedIn = true;
            AuthStatusChanged?.Invoke();
            return true; // Indicate success
        }

        public void Logout()
        {
            IsLoggedIn = false;
            //AuthStatusChanged?.Invoke();//חוזר לממסך הכניסה   
            Application.Current.Quit(); // This will close the application
        }
    }
}
