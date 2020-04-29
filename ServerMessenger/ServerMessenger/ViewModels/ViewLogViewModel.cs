using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LoggerWorker;
using ServerMessenger.Annotations;
using ServerMessenger.Comands;
using ServerMessenger.Helpers;
using ServerMessenger.Models;
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
