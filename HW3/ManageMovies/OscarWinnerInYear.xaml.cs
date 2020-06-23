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
        public List<int> yearList;
        public OscarWinnerInYear()
        {
            try
            {
                InitializeComponent();
                using (var ctx = new dbContext())
                {
                    yearList = (from o in ctx.Oscars
                                select o.Year).ToList();
                    if (yearList.Count == 0)
                    {
                        MessageBox.Show($"No data found about any oscar in DB", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    cmbYear.ItemsSource = yearList;
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbYear.SelectedIndex == -1)
                {
                    MessageBox.Show($"Must select year", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                int winnerInYear = yearList[cmbYear.SelectedIndex];
                using (var ctx = new dbContext())
                {
                    gbName.Header = $"Best movie in {winnerInYear}";
                    tbSearchResult.Text = (from o in ctx.Oscars
                                       where o.Year == winnerInYear
                                       select o.MovieSerialNavigation.Title).First().ToString();
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
