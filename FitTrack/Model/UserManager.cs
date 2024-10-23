using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            if (!_users.Any(u => u.Username == user.Username))
            {
                _users.Add(user);
            }
            else
            {
                MessageBox.Show("Username already exists.", "Error", MessageBoxButton.OK);
            }
        }

        public List<User> GetUsers()
        {
            return _users; // Return the list of users
        }
    }
}
