using ClientMessenger.ViewModels;
using System.Windows.Controls;

namespace ClientMessenger.Views
{
    /// <summary>
    /// Interaction logic for RegistrationControl.xaml
    /// </summary>
    public partial class AuthorizationControl : UserControl
    {
        public AuthorizationControl(AuthorizationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
