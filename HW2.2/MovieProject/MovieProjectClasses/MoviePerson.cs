using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MovieProjectClasses
{
    public class MoviePerson : IComparable
    {
        public enum myGender
        {
            male,
            female
        }
        private static string namePattern = @"^[A-Z][a-zA-Z]*(\s+[a-zA-Z]*)*$";

        private string firstName;
        private string lastName;
        private myGender gender;
        private DateTime birthDate;

        #region properties

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
                    throw new ArgumentException("MoviePerson's first name must contain only letters and start with a capital letter");
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
                    throw new ArgumentException("MoviePerson's last name must contain only letters and start with a capital letter");
                }
                lastName = value;
            }
        }

        public myGender Gender
        {
            get
            {
                return gender;
            }
            set
            {
                Gender = value;
            }
        }
        public DateTime BirthDate
        {
            get
            {
                return BirthDate;
            }
            set
            {
                BirthDate = value;
            }
        }

        #endregion properties

        #region constructors

        public MoviePerson(string fn, string ln, myGender gen, DateTime bd)
        {
            FirstName = fn;
            LastName = ln;
            Gender = gen;
            BirthDate = bd;
        }

        #endregion constructors

        #region overrides

        public override string ToString()
        {
            return FirstName + " " + LastName + " Is a " + Gender + " That was born on " + BirthDate;
        }

        internal MoviePerson Clone()
        {
            return new MoviePerson(FirstName, LastName, Gender, BirthDate);
        }

        public override bool Equals(object obj)
        {
            MoviePerson other = obj as MoviePerson;
            if (other == null)
            {
                throw new ArgumentException("Argument of Equals must be of type MoviePerson");
            }
            return FirstName.Equals(other.FirstName) &&
                LastName.Equals(other.LastName);
        }

        public int CompareTo(object obj)
        {
            MoviePerson a = obj as MoviePerson;
            if (a == null)
            {
                throw new ArgumentException("CompareTo must get argument of type MoviePerson");
            }
            else
            {
                int compare = lastName.CompareTo(a.lastName);
                if (compare == 0)
                {
                    return firstName.CompareTo(a.firstName);
                }
                return compare;
            }
        }

        #endregion overrides
    }
}
