using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClientMessenger.ViewModels;

namespace ClientMessenger.Views
{
    /// <summary>
    /// Interaction logic for MessengerWindow.xaml
    /// </summary>
    public partial class MessengerWindow : Window
    {
        public MessengerWindow(MessengerViewModel viewModel)
        {
            InitializeComponent();
            viewModel.CloseWindow += ViewModelOnCloseWindow;
            DataContext = viewModel;
        }

        private void ViewModelOnCloseWindow(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
