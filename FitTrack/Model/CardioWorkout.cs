using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    internal class CardioWorkout : Workout
    {
        public int Distance { get; set; }

        //constructor without Notes
        public CardioWorkout(int Distance, string Type, DateTime Date, TimeSpan Duration, int CaloriesBurned) : base(Type, Date, Duration, CaloriesBurned)
        {
            this.Distance = Distance;
        }

        //Constructor with Notes
        public CardioWorkout(int Distance, string Type, DateTime Date, TimeSpan Duration, int CaloriesBurned, string Notes) : base(Type, Date, Duration, CaloriesBurned, Notes)
        {
            this.Distance = Distance;
        }

        public override int CalculateCaloriesBurned()
        {
            //todo something more believeable
            return (Distance * 5);
        }
    }
}
