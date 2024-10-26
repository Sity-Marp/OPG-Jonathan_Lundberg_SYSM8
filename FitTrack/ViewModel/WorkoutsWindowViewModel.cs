using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.Services;
using FitTrack.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FitTrack.ViewModel
{
    internal class WorkoutsWindowViewModel
    {
        private IWindowService _windowService;
        public ICommand OpenUserDetailsWindowCommand { get; set; }
        public ICommand SignOutUserCommand { get; set; }
        public ICommand OpenAddWorkoutWindowCommand { get; set; }
        public ICommand OpenWorkoutDetailsWindowCommand { get; set; }
        public ICommand RemoveWorkoutCommand { get; set; }



        public string LoggedInUsername => User.CurrentUser?.Username ?? "Guest";


        public WorkoutsWindowViewModel(IWindowService windowService)
        {
            _windowService = windowService;

            // Opens/closes windows.
            OpenUserDetailsWindowCommand = new RelayCommand(param => UserDetailsWindow());
            SignOutUserCommand = new RelayCommand(param => SignOutUser());
            OpenAddWorkoutWindowCommand = new RelayCommand(param => AddWorkoutWindow());
            OpenWorkoutDetailsWindowCommand = new RelayCommand(param =>  WorkoutDetailsWindow());

            RemoveWorkoutCommand = new RelayCommand(param => RemoveWorkout());
        }

        private void UserDetailsWindow()
        {
            _windowService.OpenWindow<UserDetailsWindow>();
        }

        private void SignOutUser()
        {
            _windowService.OpenAndCloseWindow<MainWindow>();
        }

        private void AddWorkoutWindow()
        {
            _windowService.OpenWindow<AddWorkoutWindow>();
        }

        private void WorkoutDetailsWindow()
        {
            _windowService.OpenWindow<WorkoutDetailsWindow>();
        }

        private void RemoveWorkout()
        {
            //TODO implement
        }
    }
}
