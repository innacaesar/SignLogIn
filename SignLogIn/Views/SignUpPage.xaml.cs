using SignLogIn.ViewModels;
namespace SignLogIn
{
    public partial class SignUpPage : ContentPage
    {
       

        public SignUpPage()
        {
            InitializeComponent();
            BindingContext = new SignUpViewModel();
        }

        
    }

}
