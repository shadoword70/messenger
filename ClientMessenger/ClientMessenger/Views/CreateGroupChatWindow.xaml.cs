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
    /// Interaction logic for CreateGroupChatWindow.xaml
    /// </summary>
    public partial class CreateGroupChatWindow : Window
    {
        public CreateGroupChatWindow(CreateGroupChatViewModel viewModel)
        {
            InitializeComponent(); 
            viewModel.CloseWindow += ViewModelOnCloseWindow;
            DataContext = viewModel;
        }

        private void ViewModelOnCloseWindow(object sender, EventArgs e)
        {
            this.Close();
        }

        // Can execute
        protected void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        // Minimize
        protected void CommandBinding_Executed_Minimize(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        // Maximize
        protected void CommandBinding_Executed_Maximize(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
        }

        // Restore
        protected void CommandBinding_Executed_Restore(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.RestoreWindow(this);
        }

        // Close
        protected void CommandBinding_Executed_Close(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }
    }
}
