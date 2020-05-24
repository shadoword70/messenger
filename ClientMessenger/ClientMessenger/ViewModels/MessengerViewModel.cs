using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using ClientMessenger.Annotations;
using ClientMessenger.Commands;
using ClientMessenger.Helpers;
using ClientMessenger.Models;
using Common;
using Common.Contracts;
using ServiceWorker;

namespace ClientMessenger.ViewModels
{
    public class MessengerViewModel : INotifyPropertyChanged
    {
        public event EventHandler CloseWindow; 
        private MessengerModel _messengerData;
        public MessengerModel MessengerData
        {
            get { return _messengerData; }
            set
            {
                _messengerData = value;
                OnPropertyChanged("MessengerData");
            }
        }

        private ObservableCollection<MessengerModel> _messengerModels;

        public ObservableCollection<MessengerModel> MessengerModels
        {
            get { return _messengerModels; }
            set
            {
                _messengerModels = value;
                OnPropertyChanged("MessengerModels");
            }
        }

        private string _sendToUser;
        public string SendToUser
        {
            get { return _sendToUser; }
            set
            {
                _sendToUser = value;
                MessengerModels.Clear();
                OnPropertyChanged("SendToUser");
            }
        }

        private ObservableCollection<string> _users;

        public ObservableCollection<string> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged("Users");
            }
        }

        public string NewMessage
        {
            get { return MessengerData.NewMessage; }
            set
            {
                MessengerData.NewMessage = value;
                OnPropertyChanged("NewMessage");
            }
        }

        public string NickName
        {
            get { return MessengerData.NickName; }
            set
            {
                MessengerData.NickName = value;
                OnPropertyChanged("NickName");
            }
        }

        public int CaretPosition
        {
            get { return MessengerData.CaretPosition; }
            set
            {
                MessengerData.CaretPosition = value;
                OnPropertyChanged("CaretPosition");
            }
        }

        public MessengerViewModel(string nickName, IMessageCallback callback, List<string> users)
        {
            MessengerData = new MessengerModel();
            MessengerModels = new ObservableCollection<MessengerModel>();
            NickName = nickName;
            callback.CallbackMessage += CallbackOnCallbackMessage;
            callback.NeedUpdateUsers += CallbackOnNeedUpdateUsers;
            var manager = DIFactory.Resolve<IServiceManager>();
            manager.Disconnected += ManagerOnDisconnected;
            Users = new ObservableCollection<string>();
            foreach (var user in users)
            {
                if (user == NickName)
                {
                    continue;
                }
                Users.Add(user);
            }
        }

        private void CallbackOnNeedUpdateUsers(object sender, EventArgs e)
        {
            var users = (List<string>)sender;
            Users.Clear();
            foreach (var user in users)
            {
                if (user == NickName)
                {
                    continue;
                }
                Users.Add(user);
            }
        }

        private void ManagerOnDisconnected(object sender, EventArgs e)
        {
            var manager = DIFactory.Resolve<IServiceManager>();
            manager.Disconnect(NickName);
            CloseWindow?.Invoke(this, EventArgs.Empty);
        }

        private void CallbackOnCallbackMessage(object sender, EventArgs e)
        {
            if (sender is MessageCallbackData data)
            {
                MessengerModel mes = new MessengerModel();
                mes.Date = data.Date.ToShortDateString() + " " + data.Date.ToShortTimeString();
                mes.Nick = data.Name;
                mes.Message = data.Message;
                MessengerModels.Add(mes);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ICommand _sendMessage;

        public ICommand SendMessage
        {
            get { return _sendMessage ?? (_sendMessage = new BaseButtonCommand((obj) =>
            {
                string message = NewMessage;
                NewMessage = String.Empty;
                var manager = DIFactory.Resolve<IServiceManager>();
                manager.SendMessage(SendToUser, message);
            }, (obj) => !String.IsNullOrEmpty(NewMessage))); }
        }

        private ICommand _newLine;

        public ICommand NewLine
        {
            get
            {
                return _newLine ?? (_newLine = new BaseButtonCommand((obj) =>
                {
                    if (obj is TextBox)
                    {
                        var inputMessageBox = (TextBox)obj;
                        inputMessageBox.AppendText(Environment.NewLine);
                        inputMessageBox.CaretIndex = inputMessageBox.Text.Length;
                    }
                    
                }));
            }
        }

        private ICommand _disconnect;

        public ICommand Disconnect
        {
            get
            {
                return _disconnect ?? (_disconnect = new BaseButtonCommand((obj) =>
                {
                    var manager = DIFactory.Resolve<IServiceManager>();
                    manager.Disconnect(NickName);
                    CloseWindow?.Invoke(this, EventArgs.Empty);
                }));
            }
        }
    }
}
