using SignLogIn.Services;

namespace SignLogIn.Shells;

public partial class MainShell : Shell
{
    private readonly IAuthService _authService;
    
    public MainShell(IAuthService authService)
    {
        InitializeComponent();
        _authService = authService;
       // this.BindingContext = MainShell.Current.BindingContext;// Set the BindingContext to the MainShell's BindingContext

    }

    private void OnLogoutClicked(object sender, EventArgs e)
    {
        _authService.Logout();
    }

}