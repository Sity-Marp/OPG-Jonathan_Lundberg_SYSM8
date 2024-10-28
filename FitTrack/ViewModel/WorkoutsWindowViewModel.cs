using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.Services;
using FitTrack.View;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace FitTrack.ViewModel
{
    internal class WorkoutsWindowViewModel : ViewModelBase
    {
        private readonly IWindowService _windowService;
        private readonly WorkoutManager _workoutManager;

        public User CurrentUser { get; }

        public ICommand OpenUserDetailsWindowCommand { get; set; }
        public ICommand SignOutUserCommand { get; set; }
        public ICommand OpenAddWorkoutWindowCommand { get; set; }
        public ICommand OpenWorkoutDetailsWindowCommand { get; set; }
        public ICommand RemoveWorkoutCommand { get; set; }

        // Collection of workouts specifically for the current user
        public ObservableCollection<Workout> UserWorkouts { get; private set; }

        // Display username of the logged-in user
        public string LoggedInUsername => CurrentUser?.Username ?? "Guest";

        public WorkoutsWindowViewModel(IWindowService windowService, WorkoutManager workoutManager)
        {
            _windowService = windowService;
            _workoutManager = workoutManager;
            CurrentUser = User.CurrentUser;

            // Initialize the user's workout collection
            UserWorkouts = new ObservableCollection<Workout>(_workoutManager.GetWorkoutsForUser(CurrentUser.Username));

            // Commands for various buttons in the UI
            OpenUserDetailsWindowCommand = new RelayCommand(param => OpenUserDetailsWindow());
            SignOutUserCommand = new RelayCommand(param => SignOutUser());
            OpenAddWorkoutWindowCommand = new RelayCommand(param => OpenAddWorkoutWindow());
            OpenWorkoutDetailsWindowCommand = new RelayCommand(param => OpenWorkoutDetailsWindow());
            RemoveWorkoutCommand = new RelayCommand(param => RemoveSelectedWorkout());
        }

        private void OpenUserDetailsWindow()
        {
            _windowService.OpenWindow<UserDetailsWindow>();
        }

        private void SignOutUser()
        {
            _windowService.OpenAndCloseWindow<MainWindow>();
        }

        private void OpenAddWorkoutWindow()
        {
            // Open the AddWorkoutWindow and create a new workout
            var newWorkout = new StrengthWorkout(10, "Strength", DateTime.Now, TimeSpan.FromMinutes(30), 200, "Test workout");

            // Add the workout to the WorkoutManager and to UserWorkouts for display
            _workoutManager.AddWorkout(newWorkout);
            UserWorkouts.Add(newWorkout);
        }

        private void OpenWorkoutDetailsWindow()
        {
            _windowService.OpenWindow<WorkoutDetailsWindow>();
        }

        private void RemoveSelectedWorkout()
        {
            //// Assuming you have logic to remove the selected workout, e.g., through SelectedItem
            //if (SelectedItem != null)
            //{
            //    _workoutManager.RemoveWorkout(SelectedItem);
            //    UserWorkouts.Remove(SelectedItem);
            //}
        }
    }
}
