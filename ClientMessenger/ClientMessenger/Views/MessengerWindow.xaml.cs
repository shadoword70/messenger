using ClientMessenger.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

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
