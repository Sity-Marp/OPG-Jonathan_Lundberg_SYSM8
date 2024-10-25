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
        // Private static instance to hold the single instance of UserManager
        private static UserManager _instance;

        // Lock object for thread-safety in case of multi-threaded access.
        //This is probably overkill for this assignment, but was told it's a good idea to do so I'm doing it B).
        private static readonly object _lock = new object();

        private List<User> _users;

        // Private constructor to prevent instantiation from other classes.
        private UserManager()
        {
            _users = new List<User>(); // Initialize the list of users


            // Add test users
            _users.Add(new User("user", "password", "Canada"));
            _users.Add(new User("admin", "password", "Canada"));
        }

        // Public static property to provide global access to the instance
        public static UserManager Instance
        {
            get
            {
                // Double-check locking to ensure thread-safe singleton initialization
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new UserManager();
                        }
                    }
                }
                return _instance;
            }
        }

        //This should probably be elsewhere, check when I got internet.
        public bool ValidateUser(string username, string password)
        {
            return _users.Any(u => u.Username == username && u.Password == password);
        }


        // Adds a new user if the username is unique
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

        // Returns the list of users
        public List<User> GetUsers()
        {
            return _users;
        }
    }
}
