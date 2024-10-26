using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FitTrack.Model;
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
        public ICommand SignInCommand { get; set; }


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
            SignInCommand = new RelayCommand(param => SignIn());
        }


        private void SignIn()
        {
            // Check if credentials match any user in UserManager
            var user = UserManager.Instance.GetUsers()
                .FirstOrDefault(u => u.Username == Username && u.Password == Password);

            if (user != null)
            {
                user.SignIn(); // Set the user as the current logged-in user
                MessageBox.Show("Sign-in successful!", "Success", MessageBoxButton.OK);
                _windowService.OpenAndCloseWindow<WorkoutsWindow>();
            }
            else
            {
                MessageBox.Show("Invalid credentials. Please try again.", "Error", MessageBoxButton.OK);
            }
        }


            private void Register()
            {
                _windowService.OpenAndCloseWindow<RegisterWindow>();
            }

        private void OnCloseRegisterWindow()
        {
            _windowService.CloseWindow();
        }

    }
}
