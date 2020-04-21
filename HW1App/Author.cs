using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HW1wpfApp
{
    class Author
    {
        static string namePattern = @"^[A-Z][a-zA-Z]*(\s+[a-zA-Z]*)*$";

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
                    throw new ArgumentException("First name must contain only letters and start with capital letter");
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
                    throw new ArgumentException("Last name must contain only letters and start with capital letter");
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
                    throw new ArgumentException("Author book published must be natural number");
                }
                published = value;
            }
        }
        #endregion
        #region constructors
        public Author(string fn, string ln, int published)
        {
            FirstName = fn;
            LastName = ln;
            Published = published;
        }
        #endregion
        #region overrides
        public override string ToString()
        {
            return FirstName + " " + LastName + ", number of published books:" + Published;
        }

        internal Author Clone()
        {
            return new Author(this.FirstName, this.LastName, this.Published);
        }

        public override bool Equals(object obj)
        {
            Author other = obj as Author;
            if (other == null)
            {
                throw new ArgumentException("Argument of Equals must be of type Author");
            }
            return this.FirstName.Equals(other.FirstName) &&
                this.LastName.Equals(other.LastName);
        }
        #endregion
    }
}