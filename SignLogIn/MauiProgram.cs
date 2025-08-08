using Microsoft.Extensions.Logging;
using SignLogIn.Services;
using SignLogIn.Shells;
using SignLogIn.Views;

namespace SignLogIn
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            Routing.RegisterRoute("AdminPage", typeof(AdminPage));
            Routing.RegisterRoute("MainPage", typeof(MainPage));
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("MaterialSymbolsOutlined.ttf", "MaterialSymbols");
                });
            // Register services
            builder.Services.AddSingleton<IUserRepository, UsersDataBase>();
            builder.Services.AddTransient<ViewModels.SignUpViewModel>();
            builder.Services.AddTransient<ViewModels.LoginPageViewModel>();
            builder.Services.AddTransient<Views.SignUpPage>();
            builder.Services.AddTransient<Views.LoginPage>();
            builder.Services.AddSingleton<IAuthService, AuthService>();
            builder.Services.AddSingleton<MainShell>();
           
            builder.Services.AddTransient<Views.AdminPage>();
            builder.Services.AddTransient<ViewModels.AdminPageViewModel>();





#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
