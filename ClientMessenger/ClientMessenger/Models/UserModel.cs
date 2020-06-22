using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ClientMessenger.Annotations;

namespace ClientMessenger.Models
{
    public class UserModel : INotifyPropertyChanged
    {
        public Guid Guid { get; set; }

        private string _login;

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _surname;

        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }

        public string Patronymic { get; set; }

        public string Position { get; set; }

        public string Email { get; set; }

        private bool _isOnline;
        public bool IsOnline
        {
            get
            {
                return _isOnline;
            }
            set
            {
                _isOnline = value;
                OnPropertyChanged();
            }
        }

        public string ShortName
        {
            get
            {
                return Surname + " " + Name.Substring(0, 1).ToUpper() + ". " + Patronymic.Substring(0, 1).ToUpper() + ".";
            }
        }

        public string FullName
        {
            get
            {
                return Surname + " " + Name + " " + Patronymic;
            }
        }

        private ImageSource _employeePhoto;

        public ImageSource EmployeePhoto
        {
            get
            {
                return _employeePhoto;
            }
            set
            {
                _employeePhoto = value;
                if (_employeePhoto != null && _employeePhoto.CanFreeze)
                {
                    _employeePhoto.Freeze();
                }

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
