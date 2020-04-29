using System.Windows.Controls;
using ClientMessenger.ViewModels;

namespace ClientMessenger.Views
{
    /// <summary>
    /// Interaction logic for ConnectControl.xaml
    /// </summary>
    public partial class ConnectControl : UserControl
    {
        public ConnectControl(ConnectViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }
    }
}
