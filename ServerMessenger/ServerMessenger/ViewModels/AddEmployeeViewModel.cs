using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Common;
using Common.Enums;
using DbWorker;
using LoggerWorker;
using ServerMessenger.Annotations;
using ServerMessenger.Classes;
using ServerMessenger.Commands;
using ServerMessenger.Helpers;
using ServerMessenger.Models;
using WcfService;

namespace ServerMessenger.ViewModels
{
    public class AddEmployeeViewModel : INotifyPropertyChanged
    {
        private AddEmployeeModel _model;

        public AddEmployeeModel Model
        {
            get { return _model; }
            set
            {
                _model = value;
                OnPropertyChanged();
            }
        }

        private bool _isVisibleLoading;

        public bool IsVisibleLoading
        {
            get { return _isVisibleLoading; }
            set
            {
                _isVisibleLoading = value;
                OnPropertyChanged();
            }
        }

        public AddEmployeeViewModel()
        {
            Model = new AddEmployeeModel();
            Model.DateOfBirth = DateTime.Now;
            Model.GenderCollection = Enum.GetValues(typeof(Genders)).Cast<Genders>().ToList();
            Model.Gender = Genders.Male;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ICommand _addEmployee;
        public ICommand AddEmployee
        {
            get
            {
                return _addEmployee ?? (_addEmployee = new BaseButtonCommand(async (o) =>
                {
                    var window = o as Window;
                    if (window == null)
                    {
                        return;
                    }

                    try
                    {
                        IsVisibleLoading = true;
                        await Task.Delay(100);
                        var employee = new Employee();
                        employee.Surname = _model.Surname;
                        employee.Name = _model.Name;
                        employee.Patronymic = _model.Patronymic;
                        employee.Gender = _model.Gender;
                        employee.DateOfBirth = _model.DateOfBirth;
                        employee.Position = _model.Position;
                        employee.Email = _model.Email;
                        employee.Login = _model.Login;
                        var worker = DIFactory.Resolve<ISystemWorker>();
                        await worker.AddEmployee(employee);
                    }
                    finally
                    {
                        window.Close();
                    }
                    
                }, o => CheckModel()));
            }
        }

        private bool CheckModel()
        {
            if (String.IsNullOrEmpty(Model.Surname))
            {
                return false;
            }

            if (String.IsNullOrEmpty(Model.Name))
            {
                return false;
            }

            if (String.IsNullOrEmpty(Model.Patronymic))
            {
                return false;
            }

            if (String.IsNullOrEmpty(Model.Position))
            {
                return false;
            }

            if (String.IsNullOrEmpty(Model.Email))
            {
                return false;
            }

            if (String.IsNullOrEmpty(Model.Login))
            {
                return false;
            }

            return true;
        }

        
    }
}
