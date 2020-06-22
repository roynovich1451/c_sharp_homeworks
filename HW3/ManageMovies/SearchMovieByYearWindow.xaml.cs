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
    /// Interaction logic for SearchMovieByYearWindow.xaml
    /// </summary>
    public partial class SearchMovieByYearWindow : Window
    {
        public SearchMovieByYearWindow()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int searchCase = whichSearch();
                switch (searchCase)
                {
                    case 0: //noting entered
                        MessageBox.Show("No Year for search entered!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    case 1: //missing year in from-to
                        MessageBox.Show("For search from-to must enter both years", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    case 2: //in exact year search 
                        int exactYear = int.Parse(tbInYear.Text.Trim());
                        using (var ctx = new dbContext())
                        {
                            lbSearchResults.ItemsSource = (from m in ctx.Movies
                                                        where m.Year == exactYear
                                                        select m).ToList();
                        }
                        break;
                    case 3: //from-to searh
                        int fromYear = int.Parse(tbFromYear.Text.Trim());
                        int toYear =  int.Parse(tbToYear.Text.Trim());
                        if (fromYear > toYear)
                        {
                            MessageBox.Show("From year larger then to year!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        using (var ctx = new dbContext())
                        {
                            lbSearchResults.ItemsSource = (from m in ctx.Movies
                                                           where m.Year < toYear && m.Year > fromYear
                                                           select m).ToList();
                        }
                        break;
                    case 4: //search all
                        int fromYear2 = int.Parse(tbFromYear.Text.Trim());
                        int toYear2 = int.Parse(tbToYear.Text.Trim());
                        int exactYear2 = int.Parse(tbInYear.Text.Trim());
                        if (fromYear2 > toYear2)
                        {
                            MessageBox.Show("From year larger then to year!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        using (var ctx = new dbContext())
                        {
                            lbSearchResults.ItemsSource = (from m in ctx.Movies
                                                           where m.Year < toYear2 && m.Year > fromYear2 || m.Year == exactYear2
                                                           select m).ToList();
                        }
                        break;
                }
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

        private int whichSearch()
        {
            if (string.IsNullOrEmpty(tbFromYear.Text.Trim()) &&
                string.IsNullOrEmpty(tbInYear.Text.Trim()) &&
                string.IsNullOrEmpty(tbToYear.Text.Trim())) return 0; //nothing entered
            else if ((string.IsNullOrEmpty(tbFromYear.Text.Trim()) &&
                !string.IsNullOrEmpty(tbToYear.Text.Trim())) ||
                (!string.IsNullOrEmpty(tbFromYear.Text.Trim()) &&
                string.IsNullOrEmpty(tbToYear.Text.Trim()))) return 1; //missing year in from-to
            else if (string.IsNullOrEmpty(tbFromYear.Text.Trim()) &&
                !string.IsNullOrEmpty(tbInYear.Text.Trim()) &&
                string.IsNullOrEmpty(tbToYear.Text.Trim())) return 2; //in exact year search
            else if (!string.IsNullOrEmpty(tbFromYear.Text.Trim()) &&
                string.IsNullOrEmpty(tbInYear.Text.Trim()) &&
                !string.IsNullOrEmpty(tbToYear.Text.Trim())) return 3; //from-to searh
            else return 4; //search all
        }
    }
}
