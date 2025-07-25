using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using SignLogIn.Helper;
using SignLogIn.Services;
using SignLogIn.Views;

namespace SignLogIn.ViewModels
{
    public partial class LoginPageViewModel : ObservableObject
    {
        //private readonly UsersDataBase _database;
        private readonly IUserRepository _repository;

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
        [RelayCommand]
        private void TogglePassword()
        {
            IsPassword = !IsPassword;
            EyeIcon = IsPassword ? FontHelper.OPEN_EYE_ICON : FontHelper.CLOSED_EYE_ICON;
        }
        public LoginPageViewModel(IUserRepository repository)
        {
            // _database = new UsersDataBase();
            _repository = repository;
            // Initialize properties if needed
            Email = string.Empty;
            Password = string.Empty;
            Error = string.Empty;
            IsBusy = false; // Initially not busy


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
                await App.Current.MainPage.DisplayAlert("התחברות", "!התחברת", "OK");
                //TODO: Navigate to the special page  after successful login
                IsBusy = false; // Reset busy state
            }
            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
            {
                // Check if user exists
                var user = await _repository.GetUserByEmailAsync(Email);
                if (user != null && user.Password == Password)
                {
                    // User exists and password matches
                    Error = string.Empty; // Clear any previous error message
                    // Navigate to the main page or perform login success actions
                    await App.Current.MainPage.DisplayAlert("התחברות", "!התחברת", "OK");
                    //TODO: Navigate to the special page  after successful login
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
        private async void NavigateToRegist(SignUpPage page)
        {
            // Navigate to the sign-up page

            await App.Current.MainPage.Navigation.PushAsync(page);

        }
    }
}
