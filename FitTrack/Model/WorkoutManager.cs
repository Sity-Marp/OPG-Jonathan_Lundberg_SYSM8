using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    public class WorkoutManager
    {
        private static WorkoutManager _instance;
        private List<Workout> _workouts;

        public static WorkoutManager Instance => _instance ??= new WorkoutManager();

        public WorkoutManager()
        {
            _workouts = new List<Workout>();

            // Example of adding a test workout for "user" on startup
            if (User.CurrentUser?.Username == "user")
            {
                _workouts.Add(new StrengthWorkout(10, "Strength", DateTime.Now, TimeSpan.FromMinutes(30), 200, "Test workout"));
            }
        }

        public IEnumerable<Workout> GetAllWorkouts() => _workouts;

        public IEnumerable<Workout> GetWorkoutsForUser(string username) =>
            _workouts.Where(workout => workout.GetType().GetProperty("Username")?.GetValue(workout) as string == username);

        public void AddWorkout(Workout workout) => _workouts.Add(workout);
    }

}
