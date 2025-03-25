using Prism.Mvvm;
using System.Text.RegularExpressions;

namespace Test.Model
{
    public class User : BindableBase
    {
        public string Login { get; set; }

        private string _password;
        private string _error;

        public string Password
        {
            get => _password;
            set
            {
                if (ValidatePassword(value))
                {
                    _password = value;
                }
            }
        }

        public string Error
        {
            get => _error;
            set => SetProperty(ref _error, value);
        }
        public bool ValidatePassword(string input)
        {
            Error = string.Empty;
            if (string.IsNullOrEmpty(input))
            {
                Error = "Invalid Password";
                return false;
            }

            // Check for at least one uppercase letter and one number
            var hasUpperCase = new Regex(@"[A-Z]+");
            var hasNumber = new Regex(@"[0-9]+");

            bool isMatch = hasUpperCase.IsMatch(input) && hasNumber.IsMatch(input);
            if (!isMatch)
            {
                Error = "Invalid Password";
            }
            return isMatch;
        }
    }
}
