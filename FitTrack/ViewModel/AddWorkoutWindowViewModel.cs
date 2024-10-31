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
        private int _distance;
        private DateTime _date;
        private TimeSpan _duration;
        private int _caloriesBurned;
        private string _notes;

        public ObservableCollection<string> WorkoutTypes { get; }

        public string Type
        {
            get => _type;
            set {
                _type = value; 
                OnPropertyChanged(nameof(Type));
                OnPropertyChanged(nameof(IsStrengthWorkout));
                OnPropertyChanged(nameof(IsCardioWorkout));

            }
        }

        public int Repetitions
        {
            get => _repetitions;
            set { _repetitions = value; OnPropertyChanged(nameof(Repetitions)); }
        }
        public int Distance
        {
            get => _distance;
            set { _distance = value; OnPropertyChanged(nameof(Distance)); }
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

        public bool IsStrengthWorkout => Type == "Strength";
        public bool IsCardioWorkout => Type == "Cardio";

        public ICommand AddWorkoutCommand { get; }

        public AddWorkoutWindowViewModel(IWindowService windowService, ObservableCollection<Workout> userWorkouts)
        {
            _workoutManager = WorkoutManager.Instance;
            _windowService = windowService;
            _userWorkouts = userWorkouts;
            Date = DateTime.Now;
            AddWorkoutCommand = new RelayCommand(AddWorkout);

            WorkoutTypes = new ObservableCollection<string> { "Cardio", "Strength" };
        }

        private void AddWorkout(object parameter)
        {
            if (!ValidateFields())
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Workout newWorkout;

            if (Type == "Cardio")
            {
                newWorkout = new CardioWorkout(Repetitions, Type, Date, Duration, CaloriesBurned, Notes);
            }
            else if (Type == "Strength")
            {
                newWorkout = new StrengthWorkout(Repetitions, Type, Date, Duration, CaloriesBurned, Notes);
            }
            else
            {
                MessageBox.Show("Please select a valid workout type.");
                return;
            }

            _workoutManager.AddWorkout(newWorkout);
            _userWorkouts.Add(newWorkout);
            _windowService.CloseWindow();
        }

        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(Type))
                return false;

            if (IsStrengthWorkout && Repetitions <= 0)
                return false;

            if (IsCardioWorkout && Distance <= 0)
                return false;

            if (Date == default(DateTime))
                return false;

            if (Duration == default(TimeSpan))
                return false;

            if (CaloriesBurned <= 0)
                return false;

            if (string.IsNullOrWhiteSpace(Notes))
                return false;

            return true;
        }

    }
}
