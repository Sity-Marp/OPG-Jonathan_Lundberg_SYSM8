using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FitTrack.MVVM;
using FitTrack.Services;
using FitTrack.View;

namespace FitTrack.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private IWindowService _windowService;

        private string _username;
        private string _password;

        public ICommand OpenRegisterWindowCommand { get; set; }
        public ICommand CloseRegisterWindowCommand { get; set; }


        public string Username
        {
            get => _username;
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



        public MainWindowViewModel(IWindowService windowService)
        {
            _windowService = windowService;
            
            OpenRegisterWindowCommand = new RelayCommand(param => Register());
            CloseRegisterWindowCommand = new RelayCommand(param => OnCloseRegisterWindow());
        }

        private void Register()
        {
            _windowService.OpenWindow();

            //RegisterWindow registerwindow = new RegisterWindow
            //{
            //    DataContext = new RegisterWindowViewModel()
            //};
            //registerwindow.Show();
            Application.Current.MainWindow.Close();


        }

        private void OnCloseRegisterWindow()
        {
            _windowService.CloseWindow();
        }

    }
}
