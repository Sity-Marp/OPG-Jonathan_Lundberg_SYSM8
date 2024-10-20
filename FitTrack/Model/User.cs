using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    internal class User : Person
    {
        private string Country {  get; set; }
        private string SecurityQuestion { get; set; }
        private string SecurityAnswer { get; set; }

        public User(string Username, string Password, string Country, string SecurityQuestion, string SecurityAnswer) : base(Username, Password)
        {
            this.Country = Country;
            this.SecurityQuestion = SecurityQuestion;
            this.SecurityAnswer = SecurityAnswer;
        }

        public override void SignIn()
        {
            
        }
    }
}
