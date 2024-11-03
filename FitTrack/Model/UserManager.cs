using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private List<User> _users; // List of all users
        private Dictionary<string, ObservableCollection<Workout>> _userWorkouts; // User workouts

        // Property to hold the current user
        public User CurrentUser { get; set; }

        // Private constructor to prevent instantiation from other classes
        private UserManager()
        {
            _users = new List<User>(); // Initialize the list of users
            _userWorkouts = new Dictionary<string, ObservableCollection<Workout>>();

            // Add test users and their workouts
            var testUser = new User("user", "password", "Canada");
            _users.Add(testUser);
            InitializeUserWorkouts(testUser.Username);

            var adminUser = new Admin("admin", "password", "Canada");
            _users.Add(adminUser);
            InitializeUserWorkouts(adminUser.Username);
        }

        // Initialize workouts for a user
        private void InitializeUserWorkouts(string username)
        {
            var workouts = new ObservableCollection<Workout>();
            if (username == "user")
            {
                workouts.Add(new StrengthWorkout(10, "Strength", DateTime.Now, TimeSpan.FromMinutes(30), 200, "Test strength workout"));
                workouts.Add(new CardioWorkout(10, "Cardio", DateTime.Now, TimeSpan.FromMinutes(30), 200, "Test cardio workout"));
            }

            _userWorkouts[username] = workouts; // Store the workouts for the user
        }

        // Get workouts for the specified user
        public ObservableCollection<Workout> GetUserWorkouts(string username)
        {
            return _userWorkouts.ContainsKey(username) ? _userWorkouts[username] : new ObservableCollection<Workout>();
        }

        // Get all workouts (Admin functionality)
        public IEnumerable<Workout> GetAllWorkouts() => _userWorkouts.SelectMany(uw => uw.Value);

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
                _userWorkouts[user.Username] = new ObservableCollection<Workout>(); // Initialize workouts for the new user
            }
            else
            {
                MessageBox.Show("Username already exists.", "Error", MessageBoxButton.OK);
            }
        }

        // Updates an existing user's information, (userdetailswindow)
        public void UpdateUser(User updatedUser)
        {
            var existingUser = _users.FirstOrDefault(u => u.Username == updatedUser.Username);
            if (existingUser != null)
            {
                // Check if the username has changed
                if (existingUser.Username != updatedUser.Username)
                {
                    // Call RenameUser to handle the transfer of workouts
                    RenameUser(existingUser.Username, updatedUser.Username);
                }

                existingUser.Password = updatedUser.Password;
                existingUser.Country = updatedUser.Country;
                MessageBox.Show("User information updated successfully.", "Success", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("User not found.", "Error", MessageBoxButton.OK);
            }
        }

        // Move workouts from the old username to the new one, updates username. deletes old user 
        public void RenameUser(string oldUsername, string newUsername)
        {
            if (_userWorkouts.ContainsKey(oldUsername))
            {
                
                _userWorkouts[newUsername] = _userWorkouts[oldUsername];
                _userWorkouts.Remove(oldUsername);
            }

            var user = _users.FirstOrDefault(u => u.Username == oldUsername);
            if (user != null)
            {
                user.Username = newUsername;
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

        // Sets the current user
        public void SetCurrentUser(User user)
        {
            CurrentUser = user;
        }

        // Adds a workout for the specified user
        public void AddWorkout(string username, Workout workout)
        {
            if (_userWorkouts.ContainsKey(username))
            {
                _userWorkouts[username].Add(workout);
            }
            else
            {
                MessageBox.Show("User not found.", "Error", MessageBoxButton.OK);
            }
        }

        // Removes a workout for the specified user
        public void RemoveWorkout(string username, Workout workout)
        {
            if (_userWorkouts.ContainsKey(username) && _userWorkouts[username].Contains(workout))
            {
                _userWorkouts[username].Remove(workout);
            }
            else
            {
                MessageBox.Show("Workout not found for the specified user.", "Error", MessageBoxButton.OK);
            }
        }
    }
}
