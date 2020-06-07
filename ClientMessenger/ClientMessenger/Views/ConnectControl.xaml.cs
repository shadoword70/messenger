using ClientMessenger.ViewModels;
using System.Windows.Controls;

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
