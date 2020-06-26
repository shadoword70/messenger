using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ClientMessenger.Annotations;
using ClientMessenger.Commands;
using ClientMessenger.Helpers;
using ClientMessenger.Models;
using ClientMessenger.Views;
using Common;
using Common.Contracts;
using ServiceWorker;

namespace ClientMessenger.ViewModels
{
    public class ChangePasswordViewModel : INotifyPropertyChanged
    {
        private Guid _selfGuid;
        public event EventHandler CloseWindow;

        private ChangePasswordModel _model;

        public ChangePasswordModel Model
        {
            get { return _model; }
            set
            {
                _model = value;
                OnPropertyChanged();
            }
        }

        public ChangePasswordViewModel(Guid selfGuid)
        {
            Model = new ChangePasswordModel();
            _selfGuid = selfGuid;
        }

        public ChangePasswordViewModel()
        {
            Model = new ChangePasswordModel();
        }

        private ICommand _changePassword;
        public ICommand ChangePassword
        {
            get
            {
                return _changePassword ?? (_changePassword = new BaseButtonCommand(async (obj) =>
                {
                    if (obj is PasswordElements model)
                    {
                        if (String.IsNullOrEmpty(model.OldPassword.Password))
                        {
                            MessageBox.Show("Поле пусто!");
                            return;
                        }

                        if (String.IsNullOrEmpty(model.NewPassword.Password))
                        {
                            MessageBox.Show("Поле пусто!");
                            return;
                        }

                        if (String.IsNullOrEmpty(model.RepeatNewPassword.Password))
                        {
                            MessageBox.Show("Поле пусто!");
                            return;
                        }

                        if (model.NewPassword.Password != model.RepeatNewPassword.Password)
                        {
                            MessageBox.Show("Пароли не совпадают!");
                            return;
                        }

                        var worker = DIFactory.Resolve<IServiceManager>();
                        var result = await worker.ChangePassword(_selfGuid, model.OldPassword.Password, model.NewPassword.Password);
                        if (result.ResultStatus == ResultStatus.NotSuccess)
                        {
                            MessageBox.Show(result.Message);
                        }
                        else
                        {
                            MessageBox.Show("Пароль успешно изменен!");
                            CloseWindow?.Invoke(this, EventArgs.Empty);
                        }
                    }
                }));
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
