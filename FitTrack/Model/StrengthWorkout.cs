using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    public class StrengthWorkout : Workout
    {
        public int Repetitions { get; set; }

        //constructor without Notes
        public StrengthWorkout(int Repetitions, string Type, DateTime Date, TimeSpan Duration, int CaloriesBurned) : base(Type, Date, Duration, CaloriesBurned)
        {
            this.Repetitions = Repetitions;
        }

        //constructor with Notes
        public StrengthWorkout(int Repetitions, string Type, DateTime Date, TimeSpan Duration, int CaloriesBurned, string Notes) : base(Type, Date, Duration, CaloriesBurned, Notes)
        {
            this.Repetitions = Repetitions;
        }

        public override int CalculateCaloriesBurned()
        {
            //todo something better
            return Repetitions;
        }
    }
}
