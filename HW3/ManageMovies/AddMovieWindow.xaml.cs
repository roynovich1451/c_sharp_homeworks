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

namespace ManageMovies
{
    /// <summary>
    /// Interaction logic for AddMovieWindow.xaml
    /// </summary>
    public partial class AddMovieWindow : Window
    {
        public AddMovieWindow()
        {
            InitializeComponent();
        }

        private void btnAddMovie_Click(object sender, RoutedEventArgs e)
        {
            if (!AllTextboxesFilled())
            {
                MessageBox.Show("All text box must be filled!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            try
            {
                Movie newMovie = new Movie
                {
                    MovieSerial = tbSerial.Text.Trim(),
                    Title = tbTitle.Text.Trim(),
                    Country = tbCountry.Text.Trim(),
                    Year = int.Parse(tbYear.Text.Trim()),
                    ImdbScore = decimal.Parse(tbIMDB.Text.Trim())
                };
                using (var ctx = new dbContext())
                {
                    ctx.Movies.Add(newMovie);
                    ctx.SaveChanges();
                }
                MessageBox.Show($"{newMovie.Title}, {newMovie.Year} successfully updated in DB", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                emptyTextBoxes();
            }
            catch (FormatException)
            {
                MessageBox.Show("Data is NOT in the correct format", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("duplicate key"))
                {
                    MessageBox.Show($"Movie Serial already in use", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
        private void emptyTextBoxes()
        {
            tbSerial.Text = "";
            tbTitle.Text = "";
            tbCountry.Text = "";
            tbIMDB.Text = "";
            tbYear.Text = "";
        }

        private bool AllTextboxesFilled()
        {
            if (string.IsNullOrEmpty(tbYear.Text)) return false;
            if (string.IsNullOrEmpty(tbSerial.Text)) return false;
            if (string.IsNullOrEmpty(tbTitle.Text)) return false;
            if (string.IsNullOrEmpty(tbIMDB.Text)) return false;
            if (string.IsNullOrEmpty(tbCountry.Text)) return false;
            return true;
        }
    }
}

