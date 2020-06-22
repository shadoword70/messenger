using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using ClientMessenger.Annotations;
using ClientMessenger.Commands;
using ClientMessenger.Helpers;
using ClientMessenger.Models;
using Common.Request;
using Common.Results;
using ServiceWorker;

namespace ClientMessenger.ViewModels
{
    public class CreateGroupChatViewModel : INotifyPropertyChanged
    {
        private CreateGroupChatModel _model;

        public CreateGroupChatModel Model
        {
            get { return _model; }
            set
            {
                _model = value;
                OnPropertyChanged();
            }
        }

        private Guid _selfGuid;

        public event EventHandler CloseWindow;
        public CreateGroupChatViewModel(CreateGroupChatModel model, Guid selfGuid)
        {
            Model = model;
            _selfGuid = selfGuid;
        }

        public CreateGroupChatViewModel()
        {
            Model = new CreateGroupChatModel(new List<User>());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ICommand _createGroupChat;

        public ICommand CreateGroupChat
        {
            get
            {
                return _createGroupChat ?? (_createGroupChat = new BaseButtonCommand((o) =>
                {
                    if (o is ListBox usersListBox)
                    {
                        if (usersListBox.SelectedItems.Count == 0)
                        {
                            MessageBox.Show("Выберите участников группового чата!");
                            return;
                        }

                        if (usersListBox.SelectedItems.Count < 2)
                        {
                            MessageBox.Show("Необходимо выбрать больше 3 учасников группы!");
                            return;
                        }

                        var users = new List<UserModel>();
                        foreach (UserModel selectedItem in usersListBox.SelectedItems)
                        {
                            users.Add(selectedItem);
                        }

                        var serviceWorker = DIFactory.Resolve<IServiceManager>();
                        var request = new CreateGroupChatRequest();
                        request.ChatName = Model.ChatName;
                        request.UserGuids = users.Select(x => (Guid)x.Guid).ToList();
                        request.CreatorGuid = _selfGuid;
                        serviceWorker.CreateGroupChat(request);
                        CloseWindow?.Invoke(o, EventArgs.Empty);
                    }
                }));
            }
        }
    }
}
