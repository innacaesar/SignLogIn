using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SignLogIn.Services;

namespace SignLogIn.ViewModels
{
    [QueryProperty(nameof(PassName), "passName")]
    public partial class UserDetailsPageViewModel:ObservableObject
    {
        private readonly IUserRepository _repository;
        private readonly IServiceProvider _services;
        [ObservableProperty]
        private string passName;

        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private string email;
        [ObservableProperty]
        private string phone;
        [ObservableProperty]
        private string image;


        public UserDetailsPageViewModel(IUserRepository repository, IServiceProvider provider)
        {
            _repository = repository;
            _services = provider;

            _ = LoadUserDetails(); 
        }
        [RelayCommand]
        private async Task LoadUserDetails()
        {
           
            await Task.Delay(300); // Simulate some delay for UI responsiveness אחרת יש בעיה עם הזמן
            var user = await _repository.GetUserByNameAsync(PassName);
           
            if (user != null)
            {
                Name = user.Name;
                Email = user.Email;
                Phone = user.Phone;
                Image = user.Image;
            }
            else
            {
                // Handle the case where the user is not found
                await App.Current.MainPage.DisplayAlert("Error", "User not found", "OK");
            }
        }



    }
}
