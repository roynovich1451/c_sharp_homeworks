using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MovieProjectClasses
{
    struct MovieKey
    {
        string title;

    }
    public class Movie : IComparable
    {
        private static string titlePattern = @"^[A-Z0-9]([a-zA-z0-9]|[,.\s]){0,49}$";
        private static string yearPattern = @"^19\d\d|20[01]\d|2020$";
        private static string scorePattern = @"^[0-9](\.\d)?|10$";

        public string title;
        private MoviePerson director;
        private int year;
        private int rotTomScore;
        private decimal imdbScore;
        private List<string> actors;

        #region properties
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (!Regex.Match(value.ToString(), titlePattern).Success)
                {
                    throw new ArgumentException("title is not valid");
                }
                title = value;
            }
        }

        public MoviePerson Director
        {
            get
            {
                return director;
            }
            set
            {
                director = value;
            }
        }

        public int Year
        {
            get
            {
                return year;
            }
            set
            {
                if (!Regex.Match(value.ToString(), yearPattern).Success)
                {
                    throw new ArgumentException("valid year from 1900 to 2020");
                }
                year = value;
            }
        }

        public int RotTomScore
        {
            get
            {
                return rotTomScore;
            }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException("Rottten Tomato Score must get a natural number between 0-100");
                }
                rotTomScore = value;
            }
        }
        public decimal ImdbScore
        {
            get
            {
                return imdbScore;
            }
            set
            {
                if (!Regex.Match(value.ToString(), scorePattern).Success)
                {
                    throw new ArgumentException("ImdbScore must get a number between 0-10");
                }
                imdbScore = value;
            }
        }

        public List<string> Actors
        {
            get
            {
                return actors;
            }
            set
            {
                actors = value;
            }
        }


        #endregion properties

        #region constuctors

        public Movie(string title, MoviePerson director, int year, int rotTomScore, decimal imdbScore, List<string> actors)
        {
            Title = title;
            Director = director;
            Year = year;
            RotTomScore = rotTomScore;
            ImdbScore = imdbScore;
            Actors = actors;
        }

        #endregion constuctors

        #region overrides

        public override bool Equals(object obj)
        {
            if (!(obj is Movie other))
            {
                throw new ArgumentException("Equals argument must be of type Movie");
            }
            return Title.Equals(other.Title) && Year == other.Year;
        }

        public override string ToString()
        {
            string actorString = "{";
            foreach (var name in actors)
            {
                actorString += $"{name}, ";
            }
            actorString = actorString.Substring(0, actorString.Length - 2);
            actorString += "}";
            return $"{Title}, {Year}, Director: {Director}, Actors: {actorString}, IMDB: {ImdbScore}, Rotten Tomatoes: {RotTomScore}";
        }

        #endregion overrides

        public int CompareTo(object obj)
        {
            if (!(obj is Movie b))
            {
                throw new ArgumentException("CompareTo must get argument of type Movie");
            }
            else
            {
                return Title.CompareTo(b.Title);
            }
        }
    }
}
