using System;
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
        private static string datePattern = @"^(0?[1-9]|[12]\d|3[012])/(0?[1-9]|1[012])/(19\d{2}|20[01]\d|2020)$";
    
        private string firstName;
        private string lastName;
        public myGender Gender { get; set; }
        private String birthDate;
        public bool IsActor { get; set; }
        public bool IsDirector { get; set; }
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

        public String BirthDate
        {
            get
            {
                return birthDate;
            }
            set
            {
                if (!Regex.Match(value.ToString(), datePattern).Success)
                {
                    throw new ArgumentException("Invalid date was entered");
                }
                birthDate = value;
            }
        }

        #endregion properties

        #region constructors
        public MoviePerson(string fn, string ln, myGender gen, string bd, bool dir, bool act)
        {
            FirstName = fn;
            LastName = ln;
            Gender = gen;
            BirthDate = bd;
            IsDirector = dir;
            IsActor = act;
        }

        #endregion constructors

        #region overrides

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        internal MoviePerson Clone()
        {
            return new MoviePerson(FirstName, LastName, Gender, BirthDate, IsDirector, IsActor);
        }

        public override bool Equals(object obj)
        {
            MoviePerson other = obj as MoviePerson;
            if (other == null)
            {
                throw new ArgumentException("Argument of Equals must be of type MoviePerson");
            }
            return FirstName.Equals(other.FirstName) &&
                LastName.Equals(other.LastName) && 
                BirthDate.Equals(other.BirthDate);
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
