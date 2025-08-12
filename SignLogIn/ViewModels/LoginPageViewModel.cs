using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using SignLogIn.Helper;
using SignLogIn.Services;
using SignLogIn.Views;
using System.Diagnostics;

namespace SignLogIn.ViewModels
{
    public partial class LoginPageViewModel : ObservableObject
    {
        //private readonly UsersDataBase _database;
        private readonly IUserRepository _repository;
        private readonly IAuthService _authService; // Uncomment if you need to use AuthService
        private readonly IServiceProvider _services;

        [ObservableProperty]
        private string email;
        [ObservableProperty]
        private string password;
        [ObservableProperty]
        private string error;
        [ObservableProperty]
        private bool isPassword = true;
        [ObservableProperty]
        private string eyeIcon = FontHelper.OPEN_EYE_ICON;
        [ObservableProperty]
        private bool _isBusy;
        [ObservableProperty]
        private bool isAdmin = false; // Default to not admin
        [RelayCommand]
        private void TogglePassword()
        {
            IsPassword = !IsPassword;
            EyeIcon = IsPassword ? FontHelper.OPEN_EYE_ICON : FontHelper.CLOSED_EYE_ICON;
        }
        public LoginPageViewModel(IUserRepository repository, IAuthService authService, IServiceProvider provider)
        {
            // _database = new UsersDataBase();
            _repository = repository;
            _authService = authService;
            _services = provider;
            // Initialize properties
            Email = string.Empty;
            Password = string.Empty;
            Error = string.Empty;
            IsPassword = true; // Default to password being hidden
            EyeIcon = FontHelper.OPEN_EYE_ICON; // Default eye icon
            IsBusy = false; // Default to not busy


        }
        [RelayCommand]
        private async void GoLogin()
        {
            
            IsBusy = true; // Indicate that the app is busy (for loading indicator)
            await Task.Delay(100); // Simulate some delay for UI responsiveness

            //check if preference and secure storage is not empty
            string secureStoragePassword = await SecureStorage.GetAsync("Password") ?? string.Empty; //get the password from secure storage
            if (Preferences.ContainsKey("Email") && secureStoragePassword != null)
            {
                Email = Preferences.Get("Email", string.Empty);
                Password = await SecureStorage.GetAsync("Password") ?? "לא מצוי"; //get the password from secure storage
                
                // Navigate to the main page or perform login success actions
                //await App.Current.MainPage.DisplayAlert("התחברות", "!התחברת", "OK");
                await _authService.LoginAsync(Email, Password); // Call the login method from AuthService
                //TODO: Navigate to the special page  after successful login
                
                IsBusy = false; // Reset busy state
            }
            else
            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
            {
                // Check if user exists
                var user = await _repository.GetUserByEmailAsync(Email);

                //  לצורך הדגמה: ללא שמירת דברים בטלפון ואם יש צורך בגילוי מנהל האפליקציה
                if (user != null && user.Password == Password)
                {
                    if (Email.Equals("admin@gmail.com") && Password.Equals("admin111"))
                    {
                        // If the user is an admin, set the IsAdmin property to true
                        IsAdmin = true;
                    }
                    else
                    {
                        // If the user is not an admin, set the IsAdmin property to false
                        IsAdmin = false;
                    }
                    Preferences.Set("IsAdmin", IsAdmin); // Save admin status in preferences

                    // User exists and password matches
                    Error = string.Empty; // Clear any previous error message
                    // Navigate to the main page or perform login success actions
                    //await App.Current.MainPage.DisplayAlert("התחברות", "!התחברת", "OK");
                    await _authService.LoginAsync(Email, Password); // Call the login method from AuthService
                    //TODO: Navigate to the special page  after successful login
                   // App.Current.Windows[0].Page=new MainPage(); // Navigate to the main page
                    IsBusy = false; // Reset busy state
                }
                else
                {
                    // User does not exist or password is incorrect
                    Error = "נתוני ההתחבורת שגויים";
                }
            }
            else
            {
                Error = "יש להקיש סיסמה ומייל";
            }
            
        }
        [RelayCommand]
        private async void NavigateToRegist()
        {
            // Navigate to the sign-up page

            await App.Current.MainPage.Navigation.PushAsync(_services.GetRequiredService<SignUpPage>());
           


        }
        [RelayCommand]
        private async Task Login()
        {
            var success = await _authService.LoginAsync(Email, Password);
            if (success)
            {

                // Navigate to the main page after successful login
                await Shell.Current.GoToAsync("//MainPage");
                //await App.Current.MainPage.DisplayAlert("AI", "!התחברת", "OK");

            }
        }
        [RelayCommand]
        private async Task ResetPassword()
        {
            bool answer = await App.Current.MainPage.DisplayAlert("שחזור סיסמה", "האם לשחזר סיסמה?", "שלח sms", "בטל");
            Debug.WriteLine("Answer: " + answer);
            if (!answer)
            {
                return; // User chose to cancel the reset
            }
            // TODO implement the logic to reset the password, such as sending a password to the user's phone.
            string number = await App.Current.MainPage.DisplayPromptAsync("שחזור סיסמה", "נא להקיש את מספר הטלפון שלך", "שלח", "בטל", "מספר טלפון", -1, Keyboard.Telephone);
            Debug.WriteLine("Number: " + number);
            if (!string.IsNullOrEmpty(number))
            {
                // Check if the user exists by phone number
                var user = await _repository.GetUserByPhoneAsync(number);
                if (user != null)
                {
                    // Generate a new password (for demonstration purposes, you can implement your own logic)
                    string newPassword = GenerateRandomPassword();
                    user.Password = newPassword; // Update the user's password
                    // Save the updated user to the repository
                    await _repository.UpdateUserAsync(user);
                    // Send the new password via SMS
                    await SendSmsAsync(number, $"Your new password is: {newPassword}");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("שגיאה", "משתמש לא קיים עם מספר זה", "אישור");
                }
            }


        }

        public async Task SendSmsAsync(string number, string yourMessage)
        {
            try
            {
                var message = new SmsMessage(yourMessage, new[] { number });
                await Sms.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException ex)
            {
                // SMS is not supported on this device.
                Console.WriteLine($"SMS not supported: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Other errors.
                Console.WriteLine($"Failed to send SMS: {ex.Message}");
            }
        }

        private string GenerateRandomPassword(int length = 8)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            Random random = new Random();
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(validChars.Length)];
            }
            return new string(chars);
        }
    }
}
