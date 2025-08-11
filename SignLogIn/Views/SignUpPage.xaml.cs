using SignLogIn.ViewModels;
namespace SignLogIn.Views;

public partial class SignUpPage : ContentPage
{
	public SignUpPage(SignUpViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

    private async void GoSignIn(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//SignIn");
    }

   
}