using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ClientMessenger.Annotations;
using ClientMessenger.Helpers;
using Common.Results;

namespace ClientMessenger.Models
{
    public class ChatModel : INotifyPropertyChanged, ICloneable
    {
        public ChatModel()
        {
            Users = new ObservableCollection<UserModel>();
            Users.CollectionChanged += UsersOnCollectionChanged;
        }

        private void UsersOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("Users");
            OnPropertyChanged("TopInfoChat");
            OnPropertyChanged("ChatPhoto");
            OnPropertyChanged("BottomInfoChat");
            OnPropertyChanged("IsOnline");
        }

        public Guid ChatGuid { get; set; }

        private string _chatName;
        public string ChatName
        {
            get
            {
                return _chatName;
            }
            set
            {
                _chatName = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<UserModel> _users;
        public ObservableCollection<UserModel> Users
        {
            get
            {
                return _users;
            }
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        public ImageSource ChatPhoto
        {
            get
            {
                if (Users != null && Users.Count == 1)
                {
                    var photo = Users.First().EmployeePhoto;
                    if (photo != null && photo.CanFreeze)
                    {
                        photo.Freeze();
                        return photo;
                    }
                }

                var emptyPhotoPath = "Images\\emptyImage.png";
                if (File.Exists(emptyPhotoPath))
                {
                    var emptyPhoto = ImageHelper.GetImage(emptyPhotoPath);
                    if (emptyPhoto != null && emptyPhoto.CanFreeze)
                    {
                        emptyPhoto.Freeze();
                        return emptyPhoto;
                    }
                }

                return null;
            }
        }

        public string TopInfoChat
        {
            get
            {
                if (Users != null && Users.Count == 1)
                {
                    return Users.First().ShortName;
                }

                return ChatName;
            }
        }

        public string BottomInfoChat
        {
            get
            {
                if (Users != null && Users.Count == 1)
                {
                    return Users.First().Position;
                }

                return null;
            }
        }

        public bool IsOnline
        {
            get
            {
                if (Users != null && Users.Count == 1)
                {
                    return Users.First().IsOnline;
                }

                return false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
