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
    /// Interaction logic for AddDirectorWindow.xaml
    /// </summary>
    public partial class AddDirectorWindow : Window
    {
        public AddDirectorWindow()
        {
            InitializeComponent();
        }

        private void btnAddDirector_Click(object sender, RoutedEventArgs e)
        {
            if (!AllTextboxesFilled())
            {
                MessageBox.Show("All text box must be filled!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            try
            {
                Director newDirector = new Director
                {
                    FirstName = tbFirstName.Text.Trim(),
                    LastName = tbLastName.Text.Trim(),
                    Id = tbDirectorID.Text.Trim(),
                };
                if (newDirector.Id != null && newDirector.FirstName != null && newDirector.LastName != null)
                {
                    using (var ctx = new dbContext())
                    {
                        ctx.Directors.Add(newDirector);
                        ctx.SaveChanges();
                    }
                    MessageBox.Show($"{newDirector.FirstName} {newDirector.LastName} successfully updated in DB", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    emptyTextBoxes();
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
                    MessageBox.Show($"Director Id already in use", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            tbDirectorID.Text = "";
            tbFirstName.Text = "";
            tbLastName.Text = "";
        }

        private bool AllTextboxesFilled()
        {
            if (string.IsNullOrEmpty(tbDirectorID.Text)) return false;
            if (string.IsNullOrEmpty(tbFirstName.Text)) return false;
            if (string.IsNullOrEmpty(tbLastName.Text)) return false;
            return true;
        }
    }

}
