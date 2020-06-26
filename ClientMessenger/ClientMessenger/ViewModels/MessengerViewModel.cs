using ClientMessenger.Commands;
using ClientMessenger.Helpers;
using ClientMessenger.Models;
using Common;
using Common.Results;
using ServiceWorker;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using ClientMessenger.Properties;
using ClientMessenger.Views;
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
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MessengerModel> _messengerModels;

        public ObservableCollection<MessengerModel> MessengerModels
        {
            get { return _messengerModels; }
            set
            {
                _messengerModels = value;
                OnPropertyChanged();
            }
        }

        private ChatModel _sendToChat;
        public ChatModel SendToChat
        {
            get { return _sendToChat; }
            set
            {
                _sendToChat = value;
                GetChat?.Execute(null);
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ChatModel> _chats;

        public ObservableCollection<ChatModel> Chats
        {
            get { return _chats; }
            set
            {
                _chats = value;
                OnPropertyChanged();
            }
        }

        private object lockChat = new object();

        private string _searchUserText;
        public string SearchUserText
        {
            get { return _searchUserText; }
            set
            {
                _searchUserText = value;
                OnPropertyChanged();
            }
        }

        private string _searchMessageText;
        public string SearchMessageText
        {
            get { return _searchMessageText; }
            set
            {
                _searchMessageText = value;
                OnPropertyChanged();
            }
        }

        

        public string NewMessage
        {
            get { return MessengerData.NewMessage; }
            set
            {
                MessengerData.NewMessage = value;
                OnPropertyChanged();
            }
        }

        public UserModel CurrentUser
        {
            get { return MessengerData.CurrentUser; }
            set
            {
                MessengerData.CurrentUser = value;
                OnPropertyChanged();
            }
        }

        public int CaretPosition
        {
            get { return MessengerData.CaretPosition; }
            set
            {
                MessengerData.CaretPosition = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage _emptyPhoto;

        public MessengerViewModel(IMessageCallback callback, AuthorizationResult data)
        {
            Chats = new ObservableCollection<ChatModel>();
            Chats.CollectionChanged += ChatsOnCollectionChanged;
            MessengerData = new MessengerModel();
            var employee = data.Employee;
            MessengerModels = new ObservableCollection<MessengerModel>();
            
            var emptyPhotoPath = "Images\\emptyImage.png";
            if (File.Exists(emptyPhotoPath))
            {
                _emptyPhoto = ImageHelper.GetImage(emptyPhotoPath);
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

            CurrentUser.EmployeePhoto = ImageHelper.ByteToImageSource(employee.Photo) ?? _emptyPhoto;

            callback.CallbackMessage -= CallbackOnCallbackMessage;
            callback.NeedUpdateChats -= CallbackOnNeedUpdateChats;
            callback.CallbackMessage += CallbackOnCallbackMessage;
            callback.NeedUpdateChats += CallbackOnNeedUpdateChats;
            var manager = DIFactory.Resolve<IServiceManager>();
            manager.Disconnected += ManagerOnDisconnected;
        }

        private void ChatsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("Chats");
        }

        public MessengerViewModel()
        {
            MessengerData = new MessengerModel();
        }

        private List<User> _users;

        private void CallbackOnNeedUpdateChats(object sender, EventArgs e)
        {
            if (sender is UpdateChatsResult data)
            {
                lock (lockChat)
                {
                    if (!String.IsNullOrEmpty(SearchUserText))
                    {
                        return;
                    }

                    _users = data.Users;
                    if (SendToChat != null)
                    {
                        var removeChat = Chats.Where(x => x.Users.Count == 2).ToList();
                        foreach (var chat in removeChat)
                        {
                            Chats.Remove(chat);
                        }
                    }

                    foreach (var newChatData in data.Chats)
                    {
                        var oldChat = Chats.SingleOrDefault(x => x.ChatGuid == newChatData.ChatGuid);
                        if (oldChat != null)
                        {
                            oldChat.ChatName = newChatData.ChatName;
                            oldChat.Users.Clear();
                            foreach (var userGuid in newChatData.UserGuids)
                            {
                                if (userGuid == CurrentUser.Guid)
                                {
                                    continue;
                                }

                                var newUser = new UserModel();
                                var user = data.Users.Single(x => x.Guid == userGuid);
                                newUser.Guid = userGuid;
                                newUser.Login = user.Login;
                                newUser.Name = user.Name;
                                newUser.Surname = user.Surname;
                                newUser.Patronymic = user.Patronymic;
                                newUser.Email = user.Email;
                                newUser.IsOnline = user.IsOnline;
                                newUser.Position = user.Position;
                                newUser.EmployeePhoto =
                                    ImageHelper.ByteToImageSource(user.EmployeePhoto) ?? _emptyPhoto;

                                oldChat.Users.Add(newUser);
                            }
                        }
                        else
                        {
                            ChatModel newChat = new ChatModel();
                            newChat.ChatGuid = newChatData.ChatGuid;
                            newChat.ChatName = newChatData.ChatName;

                            foreach (var userGuid in newChatData.UserGuids)
                            {
                                if (userGuid == CurrentUser.Guid)
                                {
                                    continue;
                                }

                                var newUser = new UserModel();
                                var user = data.Users.Single(x => x.Guid == userGuid);
                                newUser.Guid = userGuid;
                                newUser.Login = user.Login;
                                newUser.Name = user.Name;
                                newUser.Surname = user.Surname;
                                newUser.Patronymic = user.Patronymic;
                                newUser.Email = user.Email;
                                newUser.IsOnline = user.IsOnline;
                                newUser.Position = user.Position;
                                newUser.EmployeePhoto =
                                    ImageHelper.ByteToImageSource(user.EmployeePhoto) ?? _emptyPhoto;

                                newChat.Users.Add(newUser);
                            }

                            Chats.Add(newChat);
                        }
                    }

                    _oldChatsModel = new ObservableCollection<ChatModel>();

                    if (Chats != null)
                    {
                        foreach (var chat in Chats)
                        {
                            _oldChatsModel.Add((ChatModel) chat.Clone());
                        }
                    }
                }
            }
        }

        private void ManagerOnDisconnected(object sender, EventArgs e)
        {
            var manager = DIFactory.Resolve<IServiceManager>();
            manager.Disconnect((Guid)CurrentUser.Guid);
            CloseWindow?.Invoke(this, EventArgs.Empty);
        }

        private void CallbackOnCallbackMessage(object sender, EventArgs e)
        {
            if (sender is MessageCallbackData data)
            {
                if (SendToChat != null && data.ChatGuid == SendToChat.ChatGuid)
                {
                    MessengerModel mes = new MessengerModel();
                    mes.Date = data.Date.ToShortDateString() + " " + data.Date.ToShortTimeString();

                    mes.Nick = _users.Single(x => x.Guid == data.SendedUserGuid).ShortName;

                    mes.Message = data.Message;
                    MessengerModels.Add(mes);
                }
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
                    manager.SendMessage(CurrentUser.Guid, SendToChat.ChatGuid, message);
                }, (obj) => !String.IsNullOrEmpty(NewMessage) && SendToChat != null));
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
                    manager.Disconnect((Guid)CurrentUser.Guid);
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
                    openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif";
                    if (openFileDialog.ShowDialog() == true)
                    {
                        filePath = openFileDialog.FileName;
                    }

                    if (String.IsNullOrEmpty(filePath))
                    {
                        return;
                    }

                    var photo = ImageHelper.GetImage(filePath);
                    var photoData = ImageHelper.ImageSourceToByte(photo);
                    var worker = DIFactory.Resolve<IServiceManager>();
                    worker.UpdatePhoto((Guid)CurrentUser.Guid, photoData);
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
                    if (SendToChat == null)
                    {
                        return;
                    }

                    var chatGuid = SendToChat.ChatGuid;
                    if (chatGuid != Guid.Empty)
                    {
                        MessengerModels.Clear();
                        var worker = DIFactory.Resolve<IServiceManager>();
                        var result = await worker.GetChat(chatGuid);
                        foreach (var message in result.Messages)
                        {
                            if (message.ChatGuid == SendToChat.ChatGuid)
                            {
                                MessengerModel mes = new MessengerModel();
                                mes.Date = message.DateCreate.ToShortDateString() + " " + message.DateCreate.ToShortTimeString();

                                mes.Nick = _users.Single(x => x.Guid == message.UserGuid).ShortName;

                                mes.Message = message.Content;
                                MessengerModels.Add(mes);
                            }
                        }
                    }
                    else
                    {
                        if (SendToChat.Users != null && SendToChat.Users.Count == 1)
                        {
                            var worker = DIFactory.Resolve<IServiceManager>();
                            var searchUserGuid = SendToChat.Users.First().Guid;
                            var existsUser = _oldChatsModel.Where(x => x.Users.Count == 1).Select(x => x.Users.First()).ToList();
                            lock (lockChat)
                            {
                                if (existsUser.Any(x => x.Guid == searchUserGuid))
                                {

                                    SearchUserText = String.Empty;
                                    Chats.Clear();
                                    foreach (var chatModel in _oldChatsModel)
                                    {
                                        Chats.Add((ChatModel) chatModel.Clone());
                                    }

                                    SendToChat = _oldChatsModel.Single(x => x.Users.Count == 1 && x.Users.First().Guid == searchUserGuid);
                                    return;
                                }

                                worker.CreateChat(SendToChat.Users.First().Guid, CurrentUser.Guid);
                                SearchUserText = null;
                            }
                        }
                    }
                }, o => SendToChat != null));
            }
        }

        private ICommand _createGroupChat;
        public ICommand CreateGroupChat
        {
            get
            {
                return _createGroupChat ?? (_createGroupChat = new BaseButtonCommand((obj) =>
                {
                    var model = new CreateGroupChatModel(_users.Where(x => x.Guid != CurrentUser.Guid).ToList());
                    var viewModel = new CreateGroupChatViewModel(model, CurrentUser.Guid);
                    var window = new CreateGroupChatWindow(viewModel);

                    if (window.ShowDialog() == true)
                    {

                    }
                }));
            }
        }

        private ICommand _searchUser;
        public ICommand SearchUser
        {
            get
            {
                return _searchUser ?? (_searchUser = new BaseButtonCommand((obj) =>
                {
                    lock (lockChat)
                    {
                        if (String.IsNullOrEmpty(SearchUserText))
                        {
                            Chats.Clear();
                            foreach (var chatModel in _oldChatsModel)
                            {
                                Chats.Add((ChatModel)chatModel.Clone());
                            }

                            return;
                        }

                        Chats.Clear(); 
                        var similarUsers = _users.Where(x => x.FullName.ToLower().Contains(SearchUserText.ToLower()) && x.Guid != CurrentUser.Guid).ToList();
                        foreach (var similarUser in similarUsers)
                        {
                            ChatModel chatModel = new ChatModel();

                            var user = new UserModel();
                            user.Guid = similarUser.Guid;
                            user.Login = similarUser.Login;
                            user.Name = similarUser.Name;
                            user.Surname = similarUser.Surname;
                            user.Patronymic = similarUser.Patronymic;
                            user.Position = similarUser.Position;
                            user.EmployeePhoto = ImageHelper.ByteToImageSource(similarUser.EmployeePhoto) ?? _emptyPhoto;
                            chatModel.Users.Add(user);
                            Chats.Add(chatModel);
                        }
                    }
                }));
            }
        }


        private ICommand _changePassword;
        public ICommand ChangePassword

        {
            get
            {
                return _changePassword ?? (_changePassword = new BaseButtonCommand((obj) =>
                {
                    var viewModel = new ChangePasswordViewModel(CurrentUser.Guid);
                    var window = new ChangePasswordWindow(viewModel);

                    if (window.ShowDialog() == true)
                    {

                    }
                }));
            }
        }

        private ICommand _searchMessage;
        public ICommand SearchMessage
        {
            get
            {
                return _searchMessage ?? (_searchMessage = new BaseButtonCommand((obj) =>
                {
                    if (obj is ListBox listBox)
                    {
                        if (!String.IsNullOrEmpty(SearchMessageText))
                        {
                            for (int i = listBox.Items.Count - 1; i >= 0; i--)
                            {
                                if (listBox.Items[i] is MessengerModel item)
                                {
                                    if (item.Message.ToLower().Contains(SearchMessageText.ToLower()))
                                    {
                                        listBox.ScrollIntoView(listBox.Items[i]);
                                        listBox.SelectedItem = listBox.Items[i];
                                        listBox.UpdateLayout();
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            listBox.ScrollIntoView(listBox.Items[listBox.Items.Count - 1]);
                            listBox.SelectedItem = null;
                            listBox.UpdateLayout();
                        }
                    }
                }));
            }
        }

        private ObservableCollection<ChatModel> _oldChatsModel;
    }
}
