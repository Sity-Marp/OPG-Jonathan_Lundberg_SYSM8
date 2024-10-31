using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FitTrack.ViewModel
{
    public class AddWorkoutWindowViewModel : ViewModelBase
    {
       
        private readonly WorkoutManager _workoutManager;
        private readonly IWindowService _windowService;
        private readonly ObservableCollection<Workout> _userWorkouts;


        private string _type;
        private int _repetitions;
        private DateTime _date;
        private TimeSpan _duration;
        private int _caloriesBurned;
        private string _notes;

        public string Type
        {
            get => _type;
            set { _type = value; OnPropertyChanged(nameof(Type)); }
        }

        public int Repetitions
        {
            get => _repetitions;
            set { _repetitions = value; OnPropertyChanged(nameof(Repetitions)); }
        }

        public DateTime Date
        {
            get => _date;
            set { _date = value; OnPropertyChanged(nameof(Date)); }
        }

        public TimeSpan Duration
        {
            get => _duration;
            set { _duration = value; OnPropertyChanged(nameof(Duration)); }
        }

        public int CaloriesBurned
        {
            get => _caloriesBurned;
            set { _caloriesBurned = value; OnPropertyChanged(nameof(CaloriesBurned)); }
        }

        public string Notes
        {
            get => _notes;
            set { _notes = value; OnPropertyChanged(nameof(Notes)); }
        }

        public ICommand AddWorkoutCommand { get; }

        public AddWorkoutWindowViewModel(IWindowService windowService, ObservableCollection<Workout> userWorkouts)
        {
            _workoutManager = WorkoutManager.Instance;
            _windowService = windowService;
            _userWorkouts = userWorkouts;
            Date = DateTime.Now;
            AddWorkoutCommand = new RelayCommand(AddWorkout);
        }

        private void AddWorkout(object parameter)
        {

            MessageBox.Show("AddWorkoutCommand executed!");

            var newWorkout = new StrengthWorkout(10, "Strength", DateTime.Now, TimeSpan.FromMinutes(30), 200, "Test workout");
            _workoutManager.AddWorkout(newWorkout);
            // Update the UserWorkouts collection in the WorkoutsWindowViewModel
            _userWorkouts.Add(newWorkout);
            _windowService.CloseWindow();

        }

    }
}
