using System.ComponentModel;
using System.Runtime.CompilerServices;
using ClientMessenger.Properties;

namespace ClientMessenger.Models
{
    public class MessengerModel : INotifyPropertyChanged
    {
        private string _nick;
        public string Nick
        {
            get { return _nick; }
            set
            {
                _nick = value;
                OnPropertyChanged("Nick");
            }
        }

        private string _date;
        public string Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged("Date");
            }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged("Message");
            }
        }

        private string _nickName;

        public string NickName
        {
            get { return _nickName; }
            set
            {
                _nickName = value;
                OnPropertyChanged("NickName");
            }
        }

        private string _newMessage;
        public string NewMessage
        {
            get { return _newMessage; }
            set
            {
                _newMessage = value;
                OnPropertyChanged("NewMessage");
            }
        }

        private int _caretPosition;
        public int CaretPosition
        {
            get { return _caretPosition; }
            set
            {
                _caretPosition = value;
                OnPropertyChanged("CaretPosition");
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
