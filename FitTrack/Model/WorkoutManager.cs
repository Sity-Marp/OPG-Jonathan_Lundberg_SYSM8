using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{//should be deleted and burnt.
    public class WorkoutManager
    {
        private static WorkoutManager _instance;
        private List<Workout> _workouts;

        public static WorkoutManager Instance => _instance ??= new WorkoutManager();

        public WorkoutManager()
        {
            _workouts = new List<Workout>();

        }

        public IEnumerable<Workout> GetAllWorkouts() => _workouts;

        public IEnumerable<Workout> GetWorkoutsForUser(string username) =>
            _workouts.Where(workout => workout.GetType().GetProperty("Username")?.GetValue(workout) as string == username);

        public void AddWorkout(Workout workout) => _workouts.Add(workout);

        public void RemoveWorkout(Workout workout) => _workouts.Remove(workout);
    }

}
