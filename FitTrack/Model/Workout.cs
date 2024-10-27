using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    internal abstract class Workout
    {
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public int CaloriesBurned { get; set; }
        public string Notes { get; set; }

        public Workout(string Type, DateTime Date, TimeSpan Duration, int CaloriesBurned)
        {
            this.Date = Date;
            this.Duration = Duration;
            this.Type = Type;
            this.CaloriesBurned = CaloriesBurned;
        }

        public Workout(string Type, DateTime Date, TimeSpan Duration, int CaloriesBurned, string Notes)
        {
            this.Date = Date;
            this.Duration = Duration;
            this.Type = Type;
            this.CaloriesBurned = CaloriesBurned;
            this.Notes = Notes;
        }


        public abstract int CalculateCaloriesBurned();

    }
}
