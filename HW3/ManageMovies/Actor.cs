using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;

namespace ManageMovies
{
    public partial class Actor
    {
        private static string namePattern = @"^[A-Z][a-zA-Z]*(\s+[a-zA-Z]*)*$";
        private static string idPattern = @"^\d{3}$";
        public Actor()
        {
            ActorMovies = new HashSet<ActorMovie>();
            OscarsBestActors = new HashSet<Oscar>();
            OscarsBestActresses = new HashSet<Oscar>();
        }

        private string id;
        private string firstName;
        private string lastName;
        private int yearBorn;
        public int Gender { get; set; }

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
        public int YearBorn
        {
            get
            {
                return yearBorn;
            }
            set
            {
                if (value < 1990 || value > 2020)
                {
                    MessageBox.Show("year born allowd: 1900-2020", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    value = -1;
                }
                yearBorn = value;
            }
        }
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
        public virtual ICollection<ActorMovie> ActorMovies { get; set; }
        public virtual ICollection<Oscar> OscarsBestActors { get; set; }
        public virtual ICollection<Oscar> OscarsBestActresses { get; set; }
    }
}
