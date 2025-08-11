using SignLogIn.ViewModels;

namespace SignLogIn.Views;

public partial class UserDetailsPage : ContentPage
{
	public UserDetailsPage(UserDetailsPageViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}