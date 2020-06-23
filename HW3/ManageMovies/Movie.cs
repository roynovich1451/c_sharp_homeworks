using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;

namespace ManageMovies
{
    public partial class Movie
    {
        private static string idPattern = @"^\d{3}$";
        private static string countryPattern = @"^[A-Z]([a-zA-Z\s])*$";
        public Movie()
        {
            ActorMovies = new HashSet<ActorMovie>();
        }

        private string movieSerial;
        public string Title { get; set; }
        private int year;
        public string DirectorId { get; set; }
        private string country;
        public decimal imdbScore;

        public virtual Director Director { get; set; }
        public virtual Oscar Oscars { get; set; }
        public virtual ICollection<ActorMovie> ActorMovies { get; set; }

        public string MovieSerial
        {
            get
            {
                return movieSerial;
            }
            set
            {
                if (!Regex.Match(value, idPattern).Success)
                {
                    MessageBox.Show("Serial must be 3 digits string", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    value = null;
                }
                movieSerial = value;
            }
        }
        public string Country
        {
            get
            {
                return country;
            }
            set
            {
                if (!Regex.Match(value, countryPattern).Success)
                {
                    MessageBox.Show("Country name must start with capital and contain only characters", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    value = null;
                }
                country = value;
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
                if (value < 1950 || value > 2020)
                {
                    MessageBox.Show("Movie's year allowed: 1950-2020", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    value = -1;
                }
                year = value;
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
                if (value < 0 || value > 10)
                {
                    MessageBox.Show("IMDB score is decimal 0-10 only", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    value = -1;
                }
                imdbScore = value;
            }
        }
        public override string ToString()
        {
            return $"{Title}, {Year}";
        }
    }
}
