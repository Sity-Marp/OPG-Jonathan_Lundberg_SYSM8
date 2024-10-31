using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.Services;
using FitTrack.View;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
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

        private Workout newWorkout;
        public Workout NewWorkout
        {
            get => newWorkout;
            set
            {
                newWorkout = value;
            }
        }

        private Workout selectedWorkout;
        public Workout SelectedWorkout
        {
            get => selectedWorkout; 
            set
            {
                selectedWorkout = value;
                OnPropertyChanged();
            }
        }

        // Collection of workouts specifically for the current user
        public ObservableCollection<Workout> UserWorkouts { get; set; }

        // Display username of the logged-in user
        public string LoggedInUsername => CurrentUser?.Username ?? "Guest";


        public WorkoutsWindowViewModel(IWindowService windowService, WorkoutManager workoutManager)
        {
            _workoutManager = WorkoutManager.Instance;
            _windowService = windowService;
            CurrentUser = User.CurrentUser;

            // Initialize the user's workout collection
            UserWorkouts = new ObservableCollection<Workout>(_workoutManager.GetWorkoutsForUser(CurrentUser.Username));

            // User user test workouts.
            if (User.CurrentUser?.Username == "user")
            {
                UserWorkouts.Add(new StrengthWorkout(10, "Strength", DateTime.Now, TimeSpan.FromMinutes(30), 200, "Test strength workout"));
                UserWorkouts.Add(new CardioWorkout(10, "Cardio", DateTime.Now, TimeSpan.FromMinutes(30), 200, "Test cardio workout"));
            }

            // Commands for various buttons in the UI
            OpenUserDetailsWindowCommand = new RelayCommand(param => OpenUserDetailsWindow());
            SignOutUserCommand = new RelayCommand(param => SignOutUser());
            OpenAddWorkoutWindowCommand = new RelayCommand(param => OpenAddWorkoutWindow());
            OpenWorkoutDetailsWindowCommand = new RelayCommand(param => OpenWorkoutDetailsWindow());
            RemoveWorkoutCommand = new RelayCommand(param => RemoveSelectedWorkout());
        }

        private void OpenUserDetailsWindow()
        {
            var CurrentUser = User.CurrentUser;
            _windowService.OpenWindow<UserDetailsWindow>();
        }

        private void SignOutUser()
        {
            _windowService.OpenAndCloseWindow<MainWindow>();
        }

        private void OpenAddWorkoutWindow()
        {
            // TEMP using the AddWorkoutWindow to create a new workout

            //var newWorkout = new StrengthWorkout(10, "Strength", DateTime.Now, TimeSpan.FromMinutes(30), 200, "Test workout");

            //// Add the workout to the WorkoutManager and to UserWorkouts for display
            //_workoutManager.AddWorkout(newWorkout);
            //UserWorkouts.Add(newWorkout);
            var addWorkoutWindow = new AddWorkoutWindow();
            var addWorkoutWindowViewModel = new AddWorkoutWindowViewModel(_windowService, UserWorkouts);
            addWorkoutWindow.DataContext = addWorkoutWindowViewModel;

            addWorkoutWindow.ShowDialog();
            //_windowService.OpenWindow<AddWorkoutWindow>();
        }

        private void OpenWorkoutDetailsWindow()
        {
            if (SelectedWorkout == null)
            {
                MessageBox.Show("Please select a workout first.");
                return;
            }

            var windowService = new WindowService();
            var workoutDetailsWindow = new WorkoutDetailsWindow();
            var workoutDetailsViewModel = new WorkoutDetailsWindowViewModel(windowService, SelectedWorkout);
            workoutDetailsWindow.DataContext = workoutDetailsViewModel;

            workoutDetailsViewModel.PropertyChanged += WorkoutDetailsViewModel_PropertyChanged;



            workoutDetailsWindow.ShowDialog();
        }

        //updates the list after the workout has been modified in the details window.
        private void WorkoutDetailsViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(WorkoutDetailsWindowViewModel.SelectedWorkout))
            {
                OnPropertyChanged(nameof(UserWorkouts));
            }
        }

        private void RemoveSelectedWorkout()
        {

            if (SelectedWorkout != null)
            {
                _workoutManager.RemoveWorkout(SelectedWorkout);
                UserWorkouts.Remove(SelectedWorkout);
            }
        }
    }
}
