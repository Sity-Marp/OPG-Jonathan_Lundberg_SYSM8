using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    public abstract class Person
    {
        //fields
        public string Username { get; set; }
        public string Password { get; set; }

        //constructor
        public Person(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }

        //abstract method
        public abstract void SignIn();
    }
}
