using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.Services;
using FitTrack.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FitTrack.ViewModel
{
    public class UserDetailsWindowViewModel : ViewModelBase
    {

        private readonly IWindowService _windowService;

        public User CurrentUser { get; private set; }
        //private IWindowService _windowService;
        //fields

        private ObservableCollection<string> _countries;
        private string _selectedCountry;
        private string _username;
        private string _password;
        private string _confirmPassword;


        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }

        }

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }

        }

        public ObservableCollection<string> Countries
        {
            get { return _countries; }
            set
            {
                _countries = value;
                OnPropertyChanged(nameof(Countries));
            }
        }

        public string SelectedCountry
        {
            get { return _selectedCountry; }
            set
            {
                _selectedCountry = value;
                OnPropertyChanged(nameof(SelectedCountry));
            }
        }
        public ICommand EditUserCommand { get; }
        public UserDetailsWindowViewModel(IWindowService windowService, User currentUser)
        {
            _windowService = windowService;
            CurrentUser = currentUser;
            LoadCountries();
            EditUserCommand = new RelayCommand(param => EditUser());
            LoadUserDetails();
        }

        private void LoadUserDetails()
        {
            // Example of loading user details into properties // thx GPT for showing me this.
            Username = CurrentUser.Username;
            SelectedCountry = CurrentUser.Country; 
        }

        // checks for when editting existting user
        private void EditUser()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmPassword) || string.IsNullOrWhiteSpace(SelectedCountry))
            {
                MessageBox.Show("Please fill all fields.", "Validation Error", MessageBoxButton.OK);
                return;
            }

            if (Password != ConfirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Validation Error", MessageBoxButton.OK);
                return;
            }

            if (Password.Length < 5)
            {
                MessageBox.Show("Password too short.", "Validation Error", MessageBoxButton.OK);
                return;
            }

            if (Username.Length <= 3)
            {
                MessageBox.Show("Username must be more than 3 characters.", "Validation Error", MessageBoxButton.OK);
                return;
            }

            var userManager = UserManager.Instance;
            var currentUser = userManager.CurrentUser;

            if (currentUser == null)
            {
                MessageBox.Show("No user is currently logged in.", "Error", MessageBoxButton.OK);
                return;
            }

            // check if the username is changing
            if (Username != currentUser.Username)
            {
                // ensure the new username is unique
                if (userManager.IsUsernameTaken(Username))
                {
                    MessageBox.Show("Username is already taken.", "Validation Error", MessageBoxButton.OK);
                    return;
                }

                // Renames the user and transfer workouts
                userManager.RenameUser(currentUser.Username, Username);
            

            // Update user properties
            currentUser.Password = Password;
            currentUser.Country = SelectedCountry;

            // Update the user in the UserManager (if necessary)
            userManager.UpdateUser(currentUser);

        }

        currentUser.Username = Username;
            currentUser.Password = Password;
            currentUser.Country = SelectedCountry;

            userManager.UpdateUser(currentUser);
            _windowService.CloseWindow();
        }


        private void LoadCountries()
        { //should be a list of every country, if not blame chatGPT
            var countries = new List<string>
            {
                  "Afghanistan", "Albania", "Algeria", "Andorra",
            "Angola", "Antigua & Deps", "Argentina", "Armenia",
            "Australia", "Austria", "Azerbaijan", "Bahamas", "Bahrain",
            "Bangladesh", "Barbados", "Belarus", "Belgium", "Belize",
            "Benin", "Bhutan", "Bolivia", "Bosnia Herzegovina", "Botswana",
            "Brazil", "Brunei", "Bulgaria", "Burkina", "Burundi", "Cambodia",
            "Cameroon", "Canada", "Cape Verde", "Central African Rep", "Chad",
            "Chile", "China", "Colombia", "Comoros", "Congo", "Congo Democratic Rep",
            "Costa Rica", "Croatia", "Cuba", "Cyprus", "Czech Republic", "Denmark",
            "Djibouti", "Dominica", "Dominican Republic", "East Timor", "Ecuador", "Egypt",
            "El Salvador", "Equatorial Guinea", "Eritrea", "Estonia", "Ethiopia", "Fiji", "Finland",
            "France", "Gabon", "Gambia", "Georgia", "Germany", "Ghana", "Greece", "Grenada", "Guatemala", "Guinea",
            "Guinea-Bissau", "Guyana", "Haiti", "Honduras", "Hungary", "Iceland", "India", "Indonesia", "Iran", "Iraq",
            "Ireland Republic", "Israel", "Italy", "Ivory Coast", "Jamaica", "Japan", "Jordan",
            "Kazakhstan", "Kenya", "Kiribati", "Korea North", "Korea South", "Kosovo", "Kuwait",
            "Kyrgyzstan", "Laos", "Latvia", "Lebanon", "Lesotho", "Liberia", "Libya",
            "Liechtenstein", "Lithuania", "Luxembourg", "Macedonia", "Madagascar", "Malawi",
            "Malaysia", "Maldives", "Mali", "Malta", "Marshall Islands", "Mauritania", "Mauritius",
            "Mexico", "Micronesia", "Moldova", "Monaco", "Mongolia", "Montenegro", "Morocco", "Mozambique",
            "Myanmar, Burma", "Namibia", "Nauru", "Nepal", "Netherlands", "New Zealand", "Nicaragua", "Niger",
            "Nigeria", "Norway", "Oman", "Pakistan", "Palau", "Panama", "Papua New Guinea", "Paraguay", "Peru",
            "Philippines", "Poland", "Portugal", "Qatar", "Romania", "Russian Federation", "Rwanda",
            "St Kitts & Nevis", "St Lucia", "Saint Vincent & the Grenadines", "Samoa", "San Marino",
            "Sao Tome & Principe", "Saudi Arabia", "Senegal", "Serbia", "Seychelles", "Sierra Leone",
            "Singapore", "Slovakia", "Slovenia", "Solomon Islands", "Somalia", "South Africa", "South Sudan",
            "Spain", "Sri Lanka", "Sudan", "Suriname", "Swaziland", "Sweden", "Switzerland", "Syria", "Taiwan",
            "Tajikistan", "Tanzania", "Thailand", "Togo", "Tonga", "Trinidad & Tobago", "Tunisia", "Turkey",
            "Turkmenistan", "Tuvalu", "Uganda", "Ukraine", "United Arab Emirates",
            "United Kingdom", "United States", "Uruguay", "Uzbekistan", "Vanuatu",
            "Vatican City", "Venezuela", "Vietnam", "Yemen", "Zambia", "Zimbabwe"
            };
            Countries = new ObservableCollection<string>(countries);
        }
    }
}
