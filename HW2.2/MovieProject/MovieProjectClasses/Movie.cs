using System;
using System.Collections.Generic;

namespace MovieProjectClasses
{
    struct MovieKey
    {
        string title;

    }
    public class Movie : IComparable
    {
        public string title;
        private MoviePerson director;
        private int year;
        private int rotTomScore;
        private double imdbScore;
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
                if (value < 1900 || value > 2020)
                {
                    throw new ArgumentException("Year must get a natural number between 1900-2020");
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
                if (value < 0 || value > 10)
                {
                    throw new ArgumentException("Rottten Tomato Score must get a natural number between 0-100");
                }
                rotTomScore = value;
            }
        }
        public double ImdbScore
        {
            get
            {
                return imdbScore;
            }
            set
            {
                if (value < 0 || value > 10)
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

        public Movie(string title, MoviePerson director, int year, int rotTomScore, double imdbScore, List<string> actors)
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
            return Title + ", " + Year;
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
