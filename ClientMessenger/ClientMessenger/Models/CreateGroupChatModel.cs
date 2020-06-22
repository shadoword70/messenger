using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ClientMessenger.Annotations;
using ClientMessenger.Helpers;
using Common.Results;

namespace ClientMessenger.Models
{
    public class CreateGroupChatModel : INotifyPropertyChanged
    {
        public CreateGroupChatModel(List<User> users)
        {
            var emptyPhotoPath = "Images\\emptyImage.png";
            BitmapImage emptyPhoto = null;
            if (File.Exists(emptyPhotoPath))
            {
                emptyPhoto = ImageHelper.GetImage(emptyPhotoPath);
            }

            Users = new ObservableCollection<UserModel>();
            foreach (var user in users)
            {
                var userModel = new UserModel();
                userModel.Guid = user.Guid;
                userModel.Login = user.Login;
                userModel.Surname = user.Surname;
                userModel.Name = user.Name;
                userModel.Patronymic = user.Patronymic;
                userModel.Position = user.Position;
                userModel.Email = user.Email;
                userModel.EmployeePhoto = ImageHelper.ByteToImageSource(user.EmployeePhoto) ?? emptyPhoto;
                Users.Add(userModel);
            }
        }

        private ObservableCollection<UserModel> _users;
        public ObservableCollection<UserModel> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        private string _chatName;
        public string ChatName
        {
            get { return _chatName; }
            set
            {
                _chatName = value;
                OnPropertyChanged();
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
