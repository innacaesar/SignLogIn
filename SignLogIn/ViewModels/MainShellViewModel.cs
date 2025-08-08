using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignLogIn.ViewModels
{
    public partial class MainShellViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isAdmin;


    
    public MainShellViewModel()
        {
            // Initialize properties if needed
            IsAdmin = Preferences.Get("IsAdmin", false); // Default to false if not set
            

        }
    }
}
