using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MovieProjectClasses;

namespace MovieApp
{
    /// <summary>
    /// Interaction logic for AddMovieWindow.xaml
    /// </summary>
    public partial class AddMovieWindow : Window
    {
        private List<string> actors;
        private MoviePerson director;
        public ObservableDictionary<MyKeyPair, Movie> movies;

        public AddMovieWindow()
        {
            InitializeComponent();
            actors = new List<string>();
            Show();
        }
        public void connecListBox(ObservableCollection<MoviePerson> dir, ObservableCollection<MoviePerson> act, ObservableDictionary<MyKeyPair, Movie> mov)
        {
            lbActors.ItemsSource = act;
            lbDirectors.ItemsSource = dir;
            movies = mov;
        }

        private void btnAddActor_Click(object sender, RoutedEventArgs e)
        {
            if (lbActors.SelectedItem == null)
            {
                    MessageBox.Show("Must select actor first", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MoviePerson selected = lbActors.SelectedItem as MoviePerson;
            MessageBoxResult res = MessageBox.Show($"Add {selected.FirstName} {selected.LastName} as movie actor?", "Confirm pick",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.No)
            {
                return;
            }
            if (actors.Count !=0 && actors.Contains(selected.ToString()) == true)
            {
                MessageBox.Show($"{selected.FirstName} {selected.LastName} already picked", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            actors.Add(selected.ToString());
        }

        private void btnAddDirector_Click(object sender, RoutedEventArgs e)
        {
            if (lbDirectors.SelectedItem == null)
            {
                MessageBox.Show("Must select director first", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MoviePerson selected = lbDirectors.SelectedItem as MoviePerson;
            MessageBoxResult res = MessageBox.Show($"Add {selected.FirstName} {selected.LastName} as movie director?", "Confirm pick",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.No)
            {
                return;
            }
            if (!string.IsNullOrEmpty(tbDirector.Text))
            {
                res = MessageBox.Show($"Replace {tbDirector.Text} with {selected.FirstName} {selected.LastName} as movie director?", "Confirm pick",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.No)
                {
                    return;
                }
            }
            tbDirector.Text = selected.ToString();
            director = selected;
        }

        private void btnSubmitMovie_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbDirector.Text) || actors.Count == 0)
                {
                    throw new Exception("Movie must have Director and Actors");
                }
                if (checkAllBoxesFilled(movieDataGrid))
                {
                    Movie temp = new Movie(
                        tbTitle.Text.Trim(),
                        director,
                        int.Parse(tbYear.Text.Trim()),
                        int.Parse(tbRottenScore.Text.Trim()),
                        decimal.Parse(tbImdbScore.Text.Trim()),
                        actors);
                    movies.Add(new MyKeyPair(temp.Title, temp.Year), temp);
                    cleanForm(movieDataGrid);
                    actors = new List<string>();
                }
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool checkAllBoxesFilled(Panel p)
        {
            if (string.IsNullOrEmpty(tbTitle.Text) ||
                string.IsNullOrEmpty(tbYear.Text) ||
                string.IsNullOrEmpty(tbRottenScore.Text)||
                string.IsNullOrEmpty(tbImdbScore.Text))
            {
                throw new Exception("All text box must be filled!");
            }
            return true;
        }

        private void cleanForm(Panel p)
        {
            foreach (var item in p.Children)
            {
                if (item is TextBox)
                {
                    TextBox tb = item as TextBox;
                    if (!string.IsNullOrEmpty(tb.Text))
                    {
                        tb.Text = "";
                    }
                }
            }
        }
    }
}