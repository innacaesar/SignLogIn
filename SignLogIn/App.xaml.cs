using SignLogIn.Services;
using SignLogIn.Shells;

namespace SignLogIn
{
    public partial class App : Application
    {
        private readonly IAuthService _authService;
        private readonly IServiceProvider _services;

        public App(IAuthService authService, IServiceProvider services)
        {
            InitializeComponent();

            _authService = authService;
            _services = services;

            _authService.AuthStatusChanged += OnAuthChanged;

            if (_authService.IsLoggedIn)
                MainPage = _services.GetRequiredService<MainShell>();
            else
                MainPage = new LoginShell();
        }

        private void OnAuthChanged()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (_authService.IsLoggedIn)
                    MainPage = _services.GetRequiredService<MainShell>();
                else
                    MainPage = new LoginShell();
            });
        }
    }
}