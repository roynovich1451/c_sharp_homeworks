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
    /// Interaction logic for OscarWinnerInYear.xaml
    /// </summary>
    public partial class OscarWinnerInYear : Window
    {
        public OscarWinnerInYear()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //TODO fixed broken app when try to search winner in year that not appear in DB
            // throw ArgumentNullException but not catch
            try
            {
                int winnerInYear = int.Parse(tbYear.Text.Trim());
                using (var ctx = new dbContext())
                {
                    string winnerID = (from o in ctx.Oscars
                                       where o.Year == winnerInYear
                                       select o.MovieSerial).First();
                    tbSearchResult.Text = (from m in ctx.Movies
                                           where m.MovieSerial == winnerID
                                           select m.Title).First().ToString();
                }
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show($"No data found in DB about asked year", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Wrong string format entered, only numbers allowed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show(ex.Message + ", Iner: " + ex.InnerException.Message + "\n" + "Type: " + ex.GetType().ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ", Iner: " + ex.InnerException.Message + "\n" + "Type: " + ex.GetType().ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
