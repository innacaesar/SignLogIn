
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SignLogIn.Models;
using SignLogIn.Services;
using SignLogIn.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignLogIn.ViewModels
{
    public partial class AdminPageViewModel : ObservableObject
    {
        private readonly IUserRepository _repository;
        private readonly IServiceProvider _services;

        [ObservableProperty]
        private ObservableCollection<User> items;
        
        [ObservableProperty]
        private string name;

        public AdminPageViewModel(IUserRepository repository, IServiceProvider provider)
        {

            _repository = repository;
            _services = provider;
            Items = new ObservableCollection<User>();
            LoadUsersAsync();

        }
        [RelayCommand]
        private async void LoadUsersAsync()
        {
            var items = await _repository.GetUsersAsync();
            Items = new ObservableCollection<User>(items);
        }
        [RelayCommand]
        private async void ItemTapped(string selectedName)
        {
            if (Items != null && Items.Count > 0)
            {
              var selectedUser = Items.FirstOrDefault(u => u.Name == selectedName); 
                
                if (selectedUser != null)
                {
                    //await App.Current.MainPage.DisplayAlert("User Selected", $"Name: {selectedUser.Name}\nEmail: {selectedUser.Email}\nPhone: {selectedUser.Phone}", "CLOSE", "DELETE USER?");

                    
                    //Navigate to the user details page UserDetailsPage
                    await Shell.Current.GoToAsync($"{nameof(UserDetailsPage)}?passName={selectedUser.Name}");
                    


                }
            }
        }

        [RelayCommand]
        private async Task DeleteUser(User userToDelete)
        {
            if (userToDelete == null)
                return;

            // מחיקה מהמסד
            await _repository.DeleteUserAsync(userToDelete);

            // מחיקה מה־ObservableCollection
            Items.Remove(userToDelete);
        }



    }
}
