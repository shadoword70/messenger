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
    public class AddEmployeeViewModel : INotifyPropertyChanged
    {
        private AddEmployeeModel _model;

        public AddEmployeeViewModel()
        {
            _model = new AddEmployeeModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
