using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;

namespace ManageMovies
{
    /// <summary>
    /// Interaction logic for MovieDetailsWindow.xaml
    /// </summary>
    public partial class MovieDetailsWindow : Window
    {
        public List<Movie> moviesInDB;
        public MovieDetailsWindow()
        {
            InitializeComponent();
            updateComboBox();
        }

        private void btnWatch_Click(object sender, RoutedEventArgs e)
        {
            if (cmbMovies.SelectedItem == null)
            {
                MessageBox.Show("Must pick movie", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Movie selectedMovie = cmbMovies.SelectedItem as Movie;
            tbSerial.Text = selectedMovie.MovieSerial;
            tbTitle.Text = selectedMovie.Title;
            tbCountry.Text = selectedMovie.Country;
            tbIMDB.Text = selectedMovie.ImdbScore.ToString();
            tbYear.Text = selectedMovie.Year.ToString();
            updateDirectorAndActorList(selectedMovie.MovieSerial, selectedMovie.DirectorId);

        }

        private void updateDirectorAndActorList(string movieSerial, string directorID)
        {
            try
            {
                using (var ctx = new dbContext())
                {
                    lbActors.ItemsSource = (from am in ctx.ActorMovie
                                            where am.MovieSerial == movieSerial
                                            join a in ctx.Actors
                                            on am.ActorId equals a.Id
                                            select a).ToList();
                    if (directorID != null)
                    {
                        tbDirector.Text = (from d in ctx.Directors
                                           where d.Id == directorID
                                           select d).First().ToString();
                    }
                    else
                    {
                        tbDirector.Text = "No director found";
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Data is NOT in the correct format", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("duplicate key"))
                {
                    MessageBox.Show($"Oscar Id already in DB", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show(ex.Message + ", Iner: " + ex.InnerException.Message + "\n" + "Type: " + ex.GetType().ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ", Iner: " + ex.InnerException.Message + "\n" + "Type: " + ex.GetType().ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void updateComboBox()
        {
            try
            {
                using (var ctx = new dbContext())
                {
                    moviesInDB = (from m in ctx.Movies
                                  select m).ToList();
                    cmbMovies.ItemsSource = moviesInDB;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Data is NOT in the correct format", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("duplicate key"))
                {
                    MessageBox.Show($"Oscar Id already in DB", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show(ex.Message + ", Iner: " + ex.InnerException.Message + "\n" + "Type: " + ex.GetType().ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ", Iner: " + ex.InnerException.Message + "\n" + "Type: " + ex.GetType().ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
