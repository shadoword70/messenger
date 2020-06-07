using ClientMessenger.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace ClientMessenger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _mainControl;
        public MainWindow()
        {
            InitializeComponent();
            _mainControl = new MainViewModel();
            DataContext = _mainControl;
            //StateChanged += MainWindowStateChangeRaised;
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

        // State change
        //protected void MainWindowStateChangeRaised(object sender, EventArgs e)
        //{
        //    if (WindowState == WindowState.Maximized)
        //    {
        //        MainWindowBorder.BorderThickness = new Thickness(8);
        //        RestoreButton.Visibility = Visibility.Visible;
        //        MaximizeButton.Visibility = Visibility.Collapsed;
        //    }
        //    else
        //    {
        //        MainWindowBorder.BorderThickness = new Thickness(0);
        //        RestoreButton.Visibility = Visibility.Collapsed;
        //        MaximizeButton.Visibility = Visibility.Visible;
        //    }
        //}
    }
}
