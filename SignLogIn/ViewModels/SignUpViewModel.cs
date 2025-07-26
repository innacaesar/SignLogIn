using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SignLogIn.Models;
using SignLogIn.Helper;
using SignLogIn.Views;
using SignLogIn.Services;

namespace SignLogIn.ViewModels
{
    public partial class SignUpViewModel : ObservableObject
    {
        //private readonly UsersDataBase _database;
        private readonly IUserRepository _repository;
        private readonly IServiceProvider _services;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string phone;

        [ObservableProperty]
        private string error;

        [ObservableProperty]
        private bool isPassword = true;

        [ObservableProperty]
        private string eyeIcon = FontHelper.OPEN_EYE_ICON;

        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private bool isChecked = false;

        public SignUpViewModel(IUserRepository repository, IServiceProvider provider)
        {
            //_database = new UsersDataBase();
            _repository = repository;
            _services = provider;
            // Initialize properties
            Name = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            Phone = string.Empty;
            Error = string.Empty;
            IsBusy = false; // Initially not busy
        }


        [RelayCommand]
        private async void SaveUser()
        {   //TODO : להעביר לכניסה
            IsBusy = true; // מסמן שהאפליקציה בתהליך (להצגת מחוון טעינה)
            await Task.Delay(100); // Simulate some delay for UI responsiveness
            if(IsChecked)
            {
                //save the email in the preferences 
                Preferences.Set("Email", Email);
                //save the password in the secure storage
                await SecureStorage.SetAsync("Password", Password);

            }

            // Check if all fields are filled
            if (!string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(name) &&
                !string.IsNullOrEmpty(email) &&
                !string.IsNullOrEmpty(phone))
            {
                // Check if user already exists
                var existingUser = await _repository.GetUserByEmailAsync(email);
                if (existingUser != null)
                {
                    //await App.Current.MainPage.DisplayAlert("שגיאה", "כבר קיים משתמש זה", "אישור");
                    Error = "המשתמש כבר קיים";
                    return;
                }
                existingUser = await _repository.GetUserByPhoneAsync(phone);
                if (existingUser != null)
                {
                    //await App.Current.MainPage.DisplayAlert("שגיאה", "מספר טלפון זה כבר בשימוש", "אישור");
                    Error = "מספר טלפון קיים";
                    return;
                }
                if (password.Length < 6)
                {
                    //await App.Current.MainPage.DisplayAlert("שגיאה", "סיסמה חייבת להכיל לפחות 6 תווים", "אישור");
                    Error = "סיסמה חייבת להכיל לפחות 6 תווים";
                    return;
                }
                if (email.Length < 5 || !email.Contains("@") || !email.Contains("."))
                {
                    //await App.Current.MainPage.DisplayAlert("שגיאה", "כתובת דוא"ל לא תקינה", "אישור");
                    Error = "כתובת הדואר אינה תקינה";
                    return;
                }

                //await App.Current.MainPage.DisplayAlert("הרשמה", "  הרשמה בוצעה בהצלחה " + Password, "אישור");
                var newUser = new User { Name = Name, Password = Password, Phone = Phone, Email = Email };
                await _repository.SaveUserAsync(newUser);
                Error = "הרשמה בוצעה בהצלחה";
                IsBusy = false; // מסיים את מצב "עסוק"

            }
            else
            {
                //await App.Current.MainPage.DisplayAlert("שגיאה", "נא למלא את כל השדות", "אישור");
                Error = "נא למלא את כל השדות";
                return;
            }
        }
        [RelayCommand]
        private void TogglePassword()
        {
            IsPassword = !IsPassword;
            EyeIcon = IsPassword ? FontHelper.OPEN_EYE_ICON : FontHelper.CLOSED_EYE_ICON;
        }

        [RelayCommand]
        private async void NavigateToLogin(LoginPage page)
        {
            // Navigate to the login page, not working with the view model

            await App.Current.MainPage.Navigation.PushAsync(_services.GetRequiredService<LoginPage>());

        }
    }
}
