using FitTrack.MVVM;
using FitTrack.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitTrack.Model;
using System.Windows.Input;
using System.Windows;

namespace FitTrack.ViewModel
{
    public class WorkoutDetailsWindowViewModel : ViewModelBase
    {
        private Workout _selectedWorkout;
        private readonly IWindowService _windowService;

        public Workout SelectedWorkout
        {
            get => _selectedWorkout;
            set
            {
                _selectedWorkout = value;
                OnPropertyChanged();
                // Notify about all related properties
                OnPropertyChanged(nameof(WorkoutType));
                OnPropertyChanged(nameof(WorkoutDate));
                OnPropertyChanged(nameof(WorkoutDuration));
                OnPropertyChanged(nameof(CaloriesBurned));
                OnPropertyChanged(nameof(Repetitions));
                OnPropertyChanged(nameof(Notes));
            }
        }

        private bool _isReadOnly = true;
        public bool IsEditable => !IsReadOnly;

        public bool IsReadOnly
        {
            get => _isReadOnly;
            set
            {
                _isReadOnly = value;
                OnPropertyChanged(nameof(IsReadOnly));
                OnPropertyChanged(nameof(IsEditable));
            }
        }

        public string WorkoutType
        {
            get => SelectedWorkout?.Type;
            set
            {
                if (SelectedWorkout != null)
                {
                    SelectedWorkout.Type = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime WorkoutDate
        {
            get => SelectedWorkout?.Date ?? DateTime.MinValue;
            set
            {
                if (SelectedWorkout != null)
                {
                    SelectedWorkout.Date = value;
                    OnPropertyChanged();
                }
            }
        }

        public TimeSpan WorkoutDuration
        {
            get => SelectedWorkout?.Duration ?? TimeSpan.Zero;
            set
            {
                if (SelectedWorkout != null)
                {
                    SelectedWorkout.Duration = value;
                    OnPropertyChanged();
                }
            }
        }

        public int CaloriesBurned
        {
            get => SelectedWorkout?.CaloriesBurned ?? 0;
            set
            {
                if (SelectedWorkout != null)
                {
                    SelectedWorkout.CaloriesBurned = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Repetitions
        {
            get => (SelectedWorkout as StrengthWorkout)?.Repetitions ?? 0;
            set
            {
                if (SelectedWorkout is StrengthWorkout strengthWorkout)
                {
                    strengthWorkout.Repetitions = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Notes
        {
            get => SelectedWorkout?.Notes;
            set
            {
                if (SelectedWorkout != null)
                {
                    SelectedWorkout.Notes = value;
                    OnPropertyChanged();
                }
            }
        }

        public WorkoutDetailsWindowViewModel(IWindowService windowService, Workout workout)
        {
            _windowService = windowService;
            SelectedWorkout = workout;
        }

        // Close command to handle the Close button click
        public ICommand CloseCommand => new RelayCommand(param => CloseWindow());
        public ICommand EditCommand => new RelayCommand(param => EditWorkout());
        public ICommand SaveCommand => new RelayCommand(param => SaveWorkout());

        private void CloseWindow()
        {
            _windowService.CloseWindow();
        }

        private void EditWorkout()
        {
            IsReadOnly = !IsReadOnly; // Toggle edit mode
        }

        private void SaveWorkout()
        {

            // Since the SelectedWorkout is a reference type,
            // the updates will automatically reflect in the workouts list in WorkoutsWindowViewModel.
            MessageBox.Show("Workout updated successfully!"); // Show confirmation
            _windowService.CloseWindow(); // Close the details window
        }
    }
}