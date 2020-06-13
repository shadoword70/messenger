using ClientMessenger.Commands;
using ClientMessenger.Helpers;
using ClientMessenger.Models;
using Common;
using Common.Results;
using ServiceWorker;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using ClientMessenger.Properties;
using Microsoft.Win32;

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

        private UserModel _sendToUser;
        public UserModel SendToUser
        {
            get { return _sendToUser; }
            set
            {
                _sendToUser = value;
                if (value != null)
                {
                    GetChat?.Execute(value.ChatGuid);
                }
                OnPropertyChanged("SendToUser");
            }
        }

        private ObservableCollection<UserModel> _users = new ObservableCollection<UserModel>();

        public ObservableCollection<UserModel> Users
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

        public UserModel CurrentUser
        {
            get { return MessengerData.CurrentUser; }
            set
            {
                MessengerData.CurrentUser = value;
                OnPropertyChanged("CurrentUser");
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

        public MessengerViewModel(IMessageCallback callback, AuthorizationResult data)
        {
            MessengerData = new MessengerModel();
            var employee = data.Employee;
            MessengerModels = new ObservableCollection<MessengerModel>();
            
            var emptyPhotoPath = "Images\\emptyImage.png";
            BitmapImage emptyPhoto = null;
            if (File.Exists(emptyPhotoPath))
            {
                emptyPhoto = ImageHelper.GetImage(emptyPhotoPath);
            }

            CurrentUser = new UserModel
            {
                Login = employee.Login,
                Name = employee.Name,
                Surname = employee.Surname,
                Patronymic = employee.Patronymic,
                Email = employee.Email,
                Guid = employee.Guid,
                Position = employee.Position
            };

            CurrentUser.EmployeePhoto = employee.Photo != null ? ImageHelper.ByteToImageSource(employee.Photo) : emptyPhoto;

            callback.CallbackMessage -= CallbackOnCallbackMessage;
            callback.NeedUpdateChats -= CallbackOnNeedUpdateChats;
            callback.CallbackMessage += CallbackOnCallbackMessage;
            callback.NeedUpdateChats += CallbackOnNeedUpdateChats;
            var manager = DIFactory.Resolve<IServiceManager>();
            manager.Disconnected += ManagerOnDisconnected;
            UpdateChatsResult dataChats = new UpdateChatsResult
            {
                Chats = data.Chats,
                Users = data.Users
            };

            CallbackOnNeedUpdateChats(dataChats, null);
        }

        private void CallbackOnNeedUpdateChats(object sender, EventArgs e)
        {
            var emptyPhotoPath = "Images\\emptyImage.png";
            BitmapImage emptyPhoto = null;
            if (File.Exists(emptyPhotoPath))
            {
                emptyPhoto = ImageHelper.GetImage(emptyPhotoPath);
            }

            if (sender is UpdateChatsResult data)
            {
                var chatGuid1 = data.Chats.Where(x => x.UserGuid == CurrentUser.Guid).ToList();
                foreach (var user in data.Users)
                {
                    if (user.Guid == CurrentUser.Guid)
                    {
                        continue;
                    }

                    Guid chatGuid2 = data.Chats.Where(x => x.UserGuid == user.Guid)
                        .Join(chatGuid1, x1 => x1.ChatGuid, x2 => x2.ChatGuid,
                            (party1, party2) => party1.ChatGuid).FirstOrDefault();

                    var oldUser = Users.SingleOrDefault(x => x.Guid == user.Guid);
                    if (oldUser != null)
                    {
                        oldUser.Login = user.Login;
                        oldUser.Guid = user.Guid;
                        oldUser.Name = user.Name;
                        oldUser.Surname = user.Surname;
                        oldUser.Patronymic = user.Patronymic;
                        oldUser.Email = user.Email;
                        oldUser.IsOnline = user.IsOnline;
                        oldUser.Position = user.Position;
                        oldUser.ChatGuid = chatGuid2 != Guid.Empty ? chatGuid2 : user.Guid;
                        oldUser.EmployeePhoto = user.EmployeePhoto != null
                            ? ImageHelper.ByteToImageSource(user.EmployeePhoto)
                            : emptyPhoto;
                    }
                    else
                    {
                        UserModel userModel = new UserModel
                        {
                            Login = user.Login,
                            Guid = user.Guid,
                            Name = user.Name,
                            Surname = user.Surname,
                            Patronymic = user.Patronymic,
                            Email = user.Email,
                            IsOnline = user.IsOnline,
                            Position = user.Position,
                    };
                        
                        userModel.ChatGuid = chatGuid2 != Guid.Empty ? chatGuid2 : user.Guid;
                        userModel.EmployeePhoto = user.EmployeePhoto != null
                            ? ImageHelper.ByteToImageSource(user.EmployeePhoto)
                            : emptyPhoto;

                        Users.Add(userModel);
                    }
                }
            }
        }

        private void ManagerOnDisconnected(object sender, EventArgs e)
        {
            var manager = DIFactory.Resolve<IServiceManager>();
            manager.Disconnect(CurrentUser.Guid);
            CloseWindow?.Invoke(this, EventArgs.Empty);
        }

        private void CallbackOnCallbackMessage(object sender, EventArgs e)
        {
            if (sender is MessageCallbackData data)
            {
                if (data.ChatGuid == SendToUser.ChatGuid)
                {
                    MessengerModel mes = new MessengerModel();
                    mes.Date = data.Date.ToShortDateString() + " " + data.Date.ToShortTimeString();
                    
                    var user = Users.SingleOrDefault(x => x.Guid == data.SendedUserGuid);
                    if (user == null)
                    {
                        mes.Nick = CurrentUser.Login;
                    }
                    else
                    {
                        mes.Nick = user.Login;
                    }

                    mes.Message = data.Message;
                    MessengerModels.Add(mes);
                }
                //else
                //{
                //    var user = Users.SingleOrDefault(x => x.Guid == data.UserGuid);
                //    if (user != null)
                //    {
                //        MessengerModel mes = new MessengerModel();
                //        mes.Date = data.Date.ToShortDateString() + " " + data.Date.ToShortTimeString();
                //        mes.Nick = user.Login;
                //        mes.Message = data.Message;
                //        MessengerModels.Add(mes);
                //    }
                //}
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
            get
            {
                return _sendMessage ?? (_sendMessage = new BaseButtonCommand((obj) =>
                {
                    string message = NewMessage;
                    NewMessage = String.Empty;
                    var manager = DIFactory.Resolve<IServiceManager>();
                    manager.SendMessage(CurrentUser.Guid, SendToUser.ChatGuid ?? SendToUser.Guid, message);
                }, (obj) => !String.IsNullOrEmpty(NewMessage)));
            }
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
                    manager.Disconnect(CurrentUser.Guid);
                    CloseWindow?.Invoke(this, EventArgs.Empty);
                }));
            }
        }

        private ICommand _loadedCommand;

        public ICommand LoadedCommand
        {
            get
            {
                return _loadedCommand ?? (_loadedCommand = new BaseButtonCommand((obj) =>
                {
                    if (obj is TextBox input)
                    {
                        input.Focus();
                    }
                }));
            }
        }

        private ICommand _updatePhoto;

        public ICommand UpdatePhoto
        {
            get
            {
                return _updatePhoto ?? (_updatePhoto = new BaseButtonCommand((obj) =>
                {
                    var filePath = String.Empty;

                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Title = "Select a picture";
                    openFileDialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
                    if (openFileDialog.ShowDialog() == true)
                    {
                        filePath = openFileDialog.FileName;
                    }

                    var photo = ImageHelper.GetImage(filePath);
                    var photoData = ImageHelper.ImageSourceToByte(photo);
                    var worker = DIFactory.Resolve<IServiceManager>();
                    worker.UpdatePhoto(CurrentUser.Guid, photoData);
                    CurrentUser.EmployeePhoto = photo;
                }));
            }
        }

        private ICommand _getChat;

        public ICommand GetChat
        {
            get
            {
                return _getChat ?? (_getChat = new BaseButtonCommand(async (obj) =>
                {
                    var chatGuid = (Guid)obj;
                    if (chatGuid != Guid.Empty)
                    {
                        MessengerModels.Clear();
                        var worker = DIFactory.Resolve<IServiceManager>();
                        var result = await worker.GetChat(chatGuid);
                        foreach (var message in result.Messages)
                        {
                            if (message.ChatGuid == SendToUser.ChatGuid)
                            {
                                MessengerModel mes = new MessengerModel();
                                mes.Date = message.DateCreate.ToShortDateString() + " " + message.DateCreate.ToShortTimeString();

                                var user = Users.SingleOrDefault(x => x.Guid == message.UserGuid);
                                if (user == null)
                                {
                                    mes.Nick = CurrentUser.Login;
                                }
                                else
                                {
                                    mes.Nick = user.Login;
                                }

                                mes.Message = message.Content;
                                MessengerModels.Add(mes);
                            }
                        }
                    }
                }));
            }
        }
    }
}
