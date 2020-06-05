using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;
using ServerMessenger.Annotations;

namespace ServerMessenger.Models
{
    public class AddEmployeeModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public Genders Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Position { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public List<Genders> GenderCollection { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Surname":
                    {
                        if (String.IsNullOrEmpty(Surname))
                        {
                            error = "Фамилия пуста";
                        }
                        break;
                    }
                    case "Name":
                    {
                        if (String.IsNullOrEmpty(Name))
                        {
                            error = "Имя пуста";
                        }
                        break;
                    }
                    case "Patronymic":
                    {
                        if (String.IsNullOrEmpty(Patronymic))
                        {
                            error = "Отчество пуста";
                        }
                        break;
                    }
                    case "Position":
                    {
                        if (String.IsNullOrEmpty(Position))
                        {
                            error = "Должность пуста";
                        }
                        break;
                    }
                    case "Email":
                    {
                        if (String.IsNullOrEmpty(Email))
                        {
                            error = "Почта пуста";
                        }
                        break;
                    }
                    case "Login":
                    {
                        if (String.IsNullOrEmpty(Login))
                        {
                            error = "Логин пуста";
                        }
                        break;
                    }
                }

                return error;
            }
        }

        public string Error { get; }
    }
}
