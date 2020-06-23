using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;

namespace ManageMovies
{
    public partial class Director
    {
        private static string namePattern = @"^[A-Z][a-zA-Z]*(\s+[a-zA-Z]*)*$";
        private static string idPattern = @"^\d{3}$";
        public Director()
        {
            Movies = new HashSet<Movie>();
            Oscars = new HashSet<Oscar>();
        }

        private string id;
        private string firstName;
        private string lastName;

        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                if (!Regex.Match(value, idPattern).Success)
                {
                    MessageBox.Show("Id must be 3 digits string", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    value = null;
                }
                id = value;
            }
        }
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if (!Regex.Match(value, namePattern).Success)
                {
                    MessageBox.Show("First name pattern is not allowed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    value = null;
                }
                firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                if (!Regex.Match(value, namePattern).Success)
                {
                    MessageBox.Show("Last name pattern is not allowed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    value = null;
                }
                lastName = value;
            }
        }
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
        public override bool Equals(object obj)
        {
            Director other = obj as Director;
            if (other == null)
            {
                throw new ArgumentException("Argument of Director.Equals must be of type Director");
            }
            return Id.Equals(other.Id);
        }
        public virtual ICollection<Movie> Movies { get; set; }
        public virtual ICollection<Oscar> Oscars { get; set; }
    }
}
