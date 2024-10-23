using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    public class User : Person
    {
        public string Country {  get; set; }
        private string SecurityQuestion { get; set; }
        private string SecurityAnswer { get; set; }

        public User(string Username, string Password, string Country, string SecurityQuestion, string SecurityAnswer) : base(Username, Password)
        {
            this.Country = Country;
            this.SecurityQuestion = SecurityQuestion;
            this.SecurityAnswer = SecurityAnswer;
        }

        public User(string Username, string Password, string Country) : base(Username, Password)
        {
            this.Country = Country;
        }

        public override void SignIn()
        {
            
        }
    }
}
