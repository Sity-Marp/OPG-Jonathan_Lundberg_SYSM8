using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    public class UserManager
    {
        private List<User> _users;

        public UserManager()
        {
            _users = new List<User>(); // Initialize the list of users
        }

        public void AddUser(User user)
        {
            _users.Add(user); // Add a user to the list
        }

        public List<User> GetUsers()
        {
            return _users; // Return the list of users
        }
    }
}
