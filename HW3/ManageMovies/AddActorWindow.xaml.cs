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
    /// Interaction logic for AddActorWindow.xaml
    /// </summary>
    public partial class AddActorWindow : Window
    {
        public AddActorWindow()
        {
            InitializeComponent();
        }


        private void btnAddActor_Click(object sender, RoutedEventArgs e)
        {
            if (!AllTextboxesFilled())
            {
                MessageBox.Show("All text box must be filled!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            try
            {
                Actor newActor = new Actor
                {
                    FirstName = tbFirstName.Text.Trim(),
                    LastName = tbLastName.Text.Trim(),
                    Id = tbActorID.Text.Trim(),
                    YearBorn = int.Parse(tbBirthYear.Text.Trim()),
                    Gender = cmbGender.SelectedIndex == 0 ? 0 : 1
                };
                using (var ctx = new dbContext())
                {
                    ctx.Actors.Add(newActor);
                    ctx.SaveChanges();
                }
                MessageBox.Show($"{newActor.FirstName} {newActor.LastName} successfully updated in DB", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                emptyTextBoxes();
            }
            catch (FormatException)
            {
                MessageBox.Show("Data is NOT in the correct format", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show(ex.Message+", Iner: "+ ex.InnerException.Message + "\n" + "Type: " + ex.GetType().ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ", Iner: " + ex.InnerException.Message + "\n" + "Type: " + ex.GetType().ToString(),"Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void emptyTextBoxes()
        {
            tbActorID.Text = "";
            tbBirthYear.Text = "";
            tbFirstName.Text = "";
            tbLastName.Text = "";
            cmbGender.SelectedIndex = -1;
        }

        private bool AllTextboxesFilled()
        {
            if (string.IsNullOrEmpty(tbActorID.Text)) return false;
            if (string.IsNullOrEmpty(tbFirstName.Text)) return false;
            if (string.IsNullOrEmpty(tbLastName.Text)) return false;
            if (string.IsNullOrEmpty(tbBirthYear.Text)) return false;
            if (cmbGender.SelectedIndex == -1) return false;
            return true;
        }

    }
}
