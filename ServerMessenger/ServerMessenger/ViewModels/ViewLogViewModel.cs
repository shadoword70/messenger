using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LoggerWorker;
using ServerHelper;
using ServerMessenger.Annotations;
using ServerMessenger.Commands;
using ServerMessenger.Helpers;
using ServerMessenger.Models;
using ServerMessenger.Windows;
using WcfService;

namespace ServerMessenger.ViewModels
{
    public class ViewLogViewModel : INotifyPropertyChanged
    {
        private MainViewModel.ChangeScreen _changeScreen;

        private ViewLogModel _logData;
        public ViewLogModel LogData
        {
            get { return _logData; }
            set
            {
                _logData = value;
                OnPropertyChanged("LogData");
            }
        }

        public string Text
        {
            get { return LogData.Text; }
            set
            {
                LogData.Text = value;
                OnPropertyChanged("Text");
            }
        }

        public ViewLogViewModel(MainViewModel.ChangeScreen changeScreen)
        {
            _changeScreen = changeScreen;
            LogData = new ViewLogModel();
            var logger = DIFactory.Resolve<ILogger>();
            logger.RaiseLogger += LoggerOnRaiseLogger;
        }

        public ViewLogViewModel()
        {
            LogData = new ViewLogModel();
        }

        private void LoggerOnRaiseLogger(object sender, string e)
        {
            Text += e + Environment.NewLine;
        }

        private ICommand _stop;
        public ICommand Stop
        {
            get
            {
                return _stop ?? (_stop = new BaseButtonCommand((obj) =>
                {
                    var manager = DIFactory.Resolve<IServiceManager>();
                    manager.StopService();
                    _changeScreen();
                }));
            }
        }
        private ICommand _addEmployee;
        public ICommand AddEmployee
        {
            get
            {
                return _addEmployee ?? (_addEmployee = new BaseButtonCommand((obj) =>
                {
                    var addEmployeeWindow = new AddEmployeeWindow(new AddEmployeeViewModel());
                    try
                    {
                        var isShow = addEmployeeWindow.ShowDialog();
                    }
                    catch (Exception e)
                    {
                        var logger = DIFactory.Resolve<ILogger>();
                        logger.Write(LogLevel.Error, e.Message, e);
                    }
                }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
