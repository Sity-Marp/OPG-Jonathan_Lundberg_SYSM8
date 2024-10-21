using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FitTrack.MVVM;
using FitTrack.View;

namespace FitTrack.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private string _username;
        private string _password;

        public string Username
        {
            get => Username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }


        private void Register()
        {

        }

    }
}
