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
                    if (String.IsNullOrEmpty(Model.OldPassword))
                    {
                        MessageBox.Show("Поле пусто!");
                        return;
                    }

                    if (String.IsNullOrEmpty(Model.NewPassword))
                    {
                        MessageBox.Show("Поле пусто!");
                        return;
                    }

                    if (String.IsNullOrEmpty(Model.RepeatNewPassword))
                    {
                        MessageBox.Show("Поле пусто!");
                        return;
                    }

                    if (Model.NewPassword != Model.RepeatNewPassword)
                    {
                        MessageBox.Show("Пароли не совпадают!");
                        return;
                    }

                    var worker = DIFactory.Resolve<IServiceManager>();
                    var result = await worker.ChangePassword(_selfGuid, Model.OldPassword, Model.NewPassword);
                    if (result.ResultStatus == ResultStatus.NotSuccess)
                    {
                        MessageBox.Show(result.Message);
                    }
                    else
                    {
                        CloseWindow?.Invoke(this, EventArgs.Empty);
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
