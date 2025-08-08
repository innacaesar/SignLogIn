using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SignLogIn.Models;
using SignLogIn.Services;
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
       
    }
}
