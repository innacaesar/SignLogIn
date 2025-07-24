using SignLogIn.ViewModels;

namespace SignLogIn.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

	private async void GoSignUp(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//SignUp");
    }
}