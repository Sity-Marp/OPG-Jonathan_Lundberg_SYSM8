using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.Services;
using FitTrack.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FitTrack.ViewModel
{
    internal class WorkoutsWindowViewModel : ViewModelBase
    {
        private List<Workout> _allWorkouts = new List<Workout>();
        private readonly User _currentUser;
        public User CurrentUser { get; }

        private IWindowService _windowService;
        public ICommand OpenUserDetailsWindowCommand { get; set; }
        public ICommand SignOutUserCommand { get; set; }
        public ICommand OpenAddWorkoutWindowCommand { get; set; }
        public ICommand OpenWorkoutDetailsWindowCommand { get; set; }
        public ICommand RemoveWorkoutCommand { get; set; }

        // Property to store the user's workouts
        public ObservableCollection<Workout> UserWorkouts { get; private set; }

        //display-purpose only
        public string LoggedInUsername => User.CurrentUser?.Username ?? "Guest"; //defaults to "guest", should one should never be able to get to it.


        public WorkoutsWindowViewModel(IWindowService windowService)
        {
            _windowService = windowService;
            CurrentUser = User.CurrentUser;

            UserWorkouts = new ObservableCollection<Workout>();
            _allWorkouts = new List<Workout>(); // Initialize an empty list for workouts

            // Check if the current user is "user" and add a sample workout if so
            if (CurrentUser.Username == "user")
            {
                var userWorkout = new StrengthWorkout(10, "Strength", DateTime.Now, TimeSpan.FromMinutes(30), 200, "Test workout");
                _allWorkouts.Add(userWorkout);
                UserWorkouts.Add(userWorkout);
            }

            // Opens/closes windows.
            OpenUserDetailsWindowCommand = new RelayCommand(param => UserDetailsWindow());
            SignOutUserCommand = new RelayCommand(param => SignOutUser());
            OpenAddWorkoutWindowCommand = new RelayCommand(param => AddWorkoutWindow());
            OpenWorkoutDetailsWindowCommand = new RelayCommand(param =>  WorkoutDetailsWindow());

            RemoveWorkoutCommand = new RelayCommand(param => RemoveWorkout());
        }

        private void LoadUserWorkouts()
        {
            // Filter workouts for the current user
            UserWorkouts.Clear();
            foreach (var workout in GetWorkoutsForCurrentUser())
            {
                UserWorkouts.Add(workout);
            }
        }

        private IEnumerable<Workout> GetWorkoutsForCurrentUser()
        {
            // Filter workouts based on the current user's identity (e.g., their Username)
            return _allWorkouts.Where(workout => workout.GetType().GetProperty("Username")?.GetValue(workout) as string == _currentUser.Username);
        }

        private void AddWorkoutWindow()
        {
            //uncomment below when addworkout window is done.
            //_windowService.OpenWindow<AddWorkoutWindow>();

            // Example workout creation
            var newWorkout = new StrengthWorkout(10, "Strength", DateTime.Now, TimeSpan.FromMinutes(30), 200, "Test workout");

            // Add the workout to the overall list and user-specific view
            _allWorkouts.Add(newWorkout);
            UserWorkouts.Add(newWorkout);
        }



        private void UserDetailsWindow()
        {
            _windowService.OpenWindow<UserDetailsWindow>();
        }

        private void SignOutUser()
        {
            _windowService.OpenAndCloseWindow<MainWindow>();
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
