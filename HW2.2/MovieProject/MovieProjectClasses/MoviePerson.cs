﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        //TODO:
        //fix regex for date
        private static string datePattern = @"^(0?[1-9]|[12][0-9]|[3][0-1])/(0?[1-9]|1[012])/([19]\d{2}|[20][01]\d|2020)$";
        //TODO:
        //remove when datePattern is ready
        private static string dummtPattern = @"^.*$";

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
                if (!Regex.Match(value.ToString(), dummtPattern).Success)
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
