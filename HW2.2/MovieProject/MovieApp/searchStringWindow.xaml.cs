using MovieProjectClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MovieApp
{
    /// <summary>
    /// Interaction logic for searchStringWindow.xaml
    /// </summary>
    public partial class searchStringWindow : Window
    {
        private ObservableCollection<string> searchResultsToShow;
        private string whichSearch;
        private ObservableDictionary<MyKeyPair, Movie> movies;
        public searchStringWindow(string whichSearch, ObservableDictionary<MyKeyPair, Movie> list)
        {
            InitializeComponent();
            tbSearch.Text = $"Search by {whichSearch}";
            searchResultsToShow = new ObservableCollection<string>();
            lbSearchResults.ItemsSource = searchResultsToShow;
            movies = list;
            this.whichSearch = whichSearch;
            Show();
        }

        private void btnSearch_click(object sender, RoutedEventArgs e)
        {
            try
            {
                calculateSearchRes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void calculateSearchRes()
        {
            searchResultsToShow.Clear();
            string value = tbValue.Text.Trim();
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("Can't search without input value");
            }
            switch (whichSearch)
            {
                case "name":
                    foreach (var mov in movies.Dict.Values)
                    {
                        if (mov.Title.StartsWith(value))
                        {
                            searchResultsToShow.Add($"{mov.title} {mov.Year}");
                        }
                    }
                    break;
                case "director":
                    foreach(var mov in movies.Dict.Values)
                    {
                        string dirName = $"{mov.Director.FirstName} {mov.Director.LastName}";
                        if (dirName.Equals(value))
                        {
                            searchResultsToShow.Add($"{mov.title} {mov.Year}");
                        }
                    }
                    break;
                case "actor":
                    foreach(var mov in movies.Dict.Values)
                    {
                        foreach(var act in mov.Actors)
                        {
                            if (act.Equals(value))
                            {
                                searchResultsToShow.Add($"{mov.title} {mov.Year}");
                            }
                        }
                    }
                    break;
                case "year":
                    if (!int.TryParse(value, out int val))
                    {
                        throw new ArgumentException("Valid year between 1900-2020");
                    }
                    foreach(var mov in movies.Dict.Values)
                    {
                        if (mov.Year.Equals(val))
                        {
                            searchResultsToShow.Add($"{mov.title} {mov.Year}");
                        }
                    }
                    break;

            }
            if(searchResultsToShow.Count == 0)
            {
                searchResultsToShow.Add("No match found");
            }
        }
    }
}
