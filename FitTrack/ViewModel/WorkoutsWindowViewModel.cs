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
        public ICommand OpenInfoButton { get; set; }

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

        private ObservableCollection<Workout> adminWorkouts;
        public ObservableCollection<Workout> AdminWorkouts
        {
            get => adminWorkouts;
            set
            {
                adminWorkouts = value;
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

            //Load all workouts
            if (CurrentUser is Admin admin)
            {
                AdminWorkouts = UserManager.Instance.GetAdminWorkouts(admin.Username);
                UserWorkouts = new ObservableCollection<Workout>(UserManager.Instance.GetAllWorkouts());

            }
            else
            {
                // If the user is not an admin, load their specific workouts
                UserWorkouts = UserManager.Instance.GetUserWorkouts(CurrentUser.Username);
                
            }

            // Commands for various buttons in the UI
            OpenUserDetailsWindowCommand = new RelayCommand(param => OpenUserDetailsWindow());
            SignOutUserCommand = new RelayCommand(param => SignOutUser());
            OpenAddWorkoutWindowCommand = new RelayCommand(param => OpenAddWorkoutWindow());
            OpenWorkoutDetailsWindowCommand = new RelayCommand(param => OpenWorkoutDetailsWindow());
            RemoveWorkoutCommand = new RelayCommand(param => RemoveSelectedWorkout());
            OpenInfoButton = new RelayCommand(param => InfoButton());
        }



        private void OpenUserDetailsWindow()
        {
            var currentUser = User.CurrentUser;
            var userDetailsWindow = new UserDetailsWindow(new UserDetailsWindowViewModel(_windowService, currentUser));
            userDetailsWindow.Show();
        }

        private void SignOutUser()
        {
            _windowService.OpenAndCloseWindow<MainWindow>();

        }
        private void InfoButton()
        {
            MessageBox.Show("this is text about FitTrack and how you use the application. I hope this is all the information you need.", "info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OpenAddWorkoutWindow()
        {
            var addWorkoutWindow = new AddWorkoutWindow();
            var addWorkoutWindowViewModel = new AddWorkoutWindowViewModel(_windowService, UserWorkouts);
            addWorkoutWindow.DataContext = addWorkoutWindowViewModel;

            addWorkoutWindow.ShowDialog();

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
                if (CurrentUser is Admin)
                {
                    var adminUsername = CurrentUser.Username;
                    _workoutManager.RemoveWorkout(SelectedWorkout);
                    UserWorkouts.Remove(SelectedWorkout);

                    // Update the AdminWorkouts if the admin is removing their own workout
                    if (AdminWorkouts.Contains(SelectedWorkout))
                    {
                        AdminWorkouts.Remove(SelectedWorkout);
                    }
                }
                else
                {
                    _workoutManager.RemoveWorkout(SelectedWorkout);
                    UserWorkouts.Remove(SelectedWorkout);
                }
            }
        }
    }
}
