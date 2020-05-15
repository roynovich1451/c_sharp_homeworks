using System;
using System.Text.RegularExpressions;

namespace HW1wpfApp
{
    internal class Author : IComparable
    {
        /// <summary>
        /// Regex pattern for correct name
        /// </summary>
        private static string namePattern = @"^[A-Z][a-zA-Z]*(\s+[a-zA-Z]*)*$";

        private string firstName;
        private string lastName;
        private int published;

        #region properties
        /// <summary>
        /// Author's first name property
        /// </summary>
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
        /// <summary>
        /// Author's last name property
        /// </summary>
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
        /// <summary>
        /// Author's number of published book property
        /// </summary>
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
        /// <summary>
        /// Author constructor, get 3 parameters
        /// </summary>
        /// <param name="fn"></param>
        /// <param name="ln"></param>
        /// <param name="published"></param>
        public Author(string fn, string ln, int published)
        {
            FirstName = fn;
            LastName = ln;
            Published = published;
        }

        #endregion constructors

        #region overrides
        /// <summary>
        /// Author ToString method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return FirstName + " " + LastName + ", number of published books:" + Published;
        }
        /// <summary>
        /// Author clone method, return new object with same data as called author
        /// </summary>
        /// <returns></returns>
        internal Author Clone()
        {
            return new Author(FirstName, LastName, Published);
        }
        /// <summary>
        /// Author equals method, compare first name and last name
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Author compareTo, compare author by lexicographic order
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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