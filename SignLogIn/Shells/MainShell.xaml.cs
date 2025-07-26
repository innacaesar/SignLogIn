using SignLogIn.Services;

namespace SignLogIn.Shells;

public partial class MainShell : Shell
{{
    private readonly IAuthService _authService;
    public MainShell(IAuthService authService)
    {
        InitializeComponent();
        _authService = authService;

    }

    private void OnLogoutClicked(object sender, EventArgs e)
    {
        _authService.Logout();
    }
}