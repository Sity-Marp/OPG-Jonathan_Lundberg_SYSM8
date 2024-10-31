using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace FitTrack.Model
{
    public class UserManager
    {
        // Private static instance to hold the single instance of UserManager
        private static UserManager _instance;

        // Lock object for thread-safety in case of multi-threaded access
        private static readonly object _lock = new object();

        private List<User> _users;

        // Property to hold the current user
        public User CurrentUser { get; set; }

        // Private constructor to prevent instantiation from other classes
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

        // Updates an existing user's information
        public void UpdateUser(User updatedUser)
        {
            var existingUser = _users.FirstOrDefault(u => u.Username == updatedUser.Username);
            if (existingUser != null)
            {
                existingUser.Password = updatedUser.Password;
                existingUser.Country = updatedUser.Country;
                MessageBox.Show("User information updated successfully.", "Success", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("User not found.", "Error", MessageBoxButton.OK);
            }
        }

        // Checks if a username is already taken
        public bool IsUsernameTaken(string username)
        {
            return _users.Any(u => u.Username == username);
        }

        // Returns the list of users
        public List<User> GetUsers()
        {
            return _users;
        }
    }
}
