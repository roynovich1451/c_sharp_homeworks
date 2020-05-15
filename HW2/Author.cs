using System;
using System.Text.RegularExpressions;

namespace HW1wpfApp
{
    internal class Author : IComparable
    {
        private static string namePattern = @"^[A-Z][a-zA-Z]*(\s+[a-zA-Z]*)*$";

        private string firstName;
        private string lastName;
        private int published;

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
                    throw new ArgumentException("Author's first name must contain only letters and start with a capital letter");
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
                    throw new ArgumentException("Author's last name must contain only letters and start with a capital letter");
                }
                lastName = value;
            }
        }

        public int Published
        {
            get
            {
                return published;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Author's number of book published must be a natural number");
                }
                published = value;
            }
        }

        #endregion properties

        #region constructors

        public Author(string fn, string ln, int published)
        {
            FirstName = fn;
            LastName = ln;
            Published = published;
        }

        #endregion constructors

        #region overrides

        public override string ToString()
        {
            return FirstName + " " + LastName + ", number of published books:" + Published;
        }

        internal Author Clone()
        {
            return new Author(FirstName, LastName, Published);
        }

        public override bool Equals(object obj)
        {
            Author other = obj as Author;
            if (other == null)
            {
                throw new ArgumentException("Argument of Equals must be of type Author");
            }
            return FirstName.Equals(other.FirstName) &&
                LastName.Equals(other.LastName);
        }

        public int CompareTo(object obj)
        {
            Author a = obj as Author;
            if (a == null)
            {
                throw new ArgumentException("CompareTo must get argument of type Author");
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