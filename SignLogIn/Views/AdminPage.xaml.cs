using SignLogIn.ViewModels;

namespace SignLogIn.Views;

public partial class AdminPage : ContentPage
{
	public AdminPage(AdminPageViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;

    }
}