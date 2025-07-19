using SignLogIn.ViewModels;
namespace SignLogIn
{
    public partial class MainPage : ContentPage
    {
       

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

        
    }

}
