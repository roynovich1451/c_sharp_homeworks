using MovieProjectClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        private ObservableCollection<string> searchResultsToShow;
        private string whichSearch;
        private ObservableDictionary<MyKeyPair, Movie> movies;
        public SearchWindow(string whichSearch, ObservableDictionary<MyKeyPair, Movie> list)
        {
            InitializeComponent();
            searchResultsToShow = new ObservableCollection<string>();
            lbSearchResults.ItemsSource = searchResultsToShow;
            movies = list;
            this.whichSearch = whichSearch;
            Show();
        }

        #region clicks
        private void btnGT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                calculateSearchRes("GT", whichSearch, tbValue.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnLT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                calculateSearchRes("LT", whichSearch, tbValue.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnGET_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                calculateSearchRes("GET", whichSearch, tbValue.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnLET_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                calculateSearchRes("LET", whichSearch, tbValue.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region helpers
        /// <summary>
        /// search movies in list for IMDB or Rotten Tomatoes scores
        /// </summary>
        /// <param name="op"></param>
        /// <param name="which"></param>
        /// <param name="input"></param>
        private void calculateSearchRes(string op, string which, string input)
        {
            searchResultsToShow.Clear();
            switch (whichSearch)
            {
                case "Rotten":
                    if (!int.TryParse(input, out int intVal))
                    {
                        throw new ArgumentException("Valid Rotten Tomatoes search value is natural number between 0-100");
                    }
                    if (intVal > 100 || intVal < 0)
                    {
                        throw new ArgumentException("Valid Rotten Tomatoes search value is natural number between 0-100");
                    }
                    switch (op)
                    {
                        case "LT":
                            foreach (var mov in movies.Dict.Values)
                                if (mov.RotTomScore < intVal)
                                {
                                    searchResultsToShow.Add($"{mov.Title}, Rotten score: {mov.RotTomScore}");
                                }
                            break;
                        case "GT":
                            foreach (var mov in movies.Dict.Values)
                                if (mov.RotTomScore > intVal)
                                {
                                    searchResultsToShow.Add($"{mov.Title}, Rotten score: {mov.RotTomScore}");
                                }
                            break;
                        case "LET":
                            foreach (var mov in movies.Dict.Values)
                                if (mov.RotTomScore <= intVal)
                                {
                                    searchResultsToShow.Add($"{mov.Title}, Rotten score: {mov.RotTomScore}");
                                }
                            break;
                        case "GET":
                            foreach (var mov in movies.Dict.Values)
                                if (mov.RotTomScore >= intVal)
                                {
                                    searchResultsToShow.Add($"{mov.Title}, Rotten score: {mov.RotTomScore}");
                                }
                            break;
                    }
                    if (searchResultsToShow.Count == 0)
                    {
                        searchResultsToShow.Add("No matches found");
                    }
                    break;
                case "IMDB":
                    if (!decimal.TryParse(input, out decimal decVal))
                    {
                        throw new ArgumentException("Valid IMDB search value is decimal number between 0-10");
                    }
                    if (decVal > 10 || decVal < 0)
                    {
                        throw new ArgumentException("Valid IMDB search value is decimal number between 0-10");
                    }
                    switch (op)
                    {
                        case "LT":
                            foreach (var mov in movies.Dict.Values)
                                if (mov.ImdbScore < decVal)
                                {
                                    searchResultsToShow.Add($"{mov.Title}, IMDB score: {mov.ImdbScore}");
                                }
                            break;
                        case "GT":
                            foreach (var mov in movies.Dict.Values)
                                if (mov.ImdbScore > decVal)
                                {
                                    searchResultsToShow.Add($"{mov.Title}, IMDB score: {mov.ImdbScore}");
                                }
                            break;
                        case "LET":
                            foreach (var mov in movies.Dict.Values)
                                if (mov.ImdbScore <= decVal)
                                {
                                    searchResultsToShow.Add($"{mov.Title}, IMDB score: {mov.ImdbScore}");
                                }
                            break;
                        case "GET":
                            foreach (var mov in movies.Dict.Values)
                                if (mov.ImdbScore >= decVal)
                                {
                                    searchResultsToShow.Add($"{mov.Title}, IMDB score: {mov.ImdbScore}");
                                }
                            break;

                    }
                    break;
            }
            if (searchResultsToShow.Count == 0)
            {
                searchResultsToShow.Add("No matches found");
            }
        }
        #endregion
    }
}
