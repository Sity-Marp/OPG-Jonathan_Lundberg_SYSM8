using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    public class Admin : User
    {

        public Admin(string Username, string Password, string Country) : base(Username, Password, Country)
        {

        }

        //ran out of time and this ended up being handled by UserManager. =/
        public void ManageAllWorkouts()
        {
            WorkoutManager.Instance.GetAllWorkouts();
        }
    }
}
