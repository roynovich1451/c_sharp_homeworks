using MovieProjectClasses;
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
using static MovieProjectClasses.MoviePerson;

namespace MovieApp
{
    /// <summary>
    /// Interaction logic for AddMoviePersonWindow.xaml
    /// </summary>
    public partial class AddMoviePersonWindow : Window
    {
        public MoviePerson newPerson;
        public AddMoviePersonWindow()
        {
            InitializeComponent();
        }
        private bool generatePerson() 
        {
            try
            {
                if (!checkAllBoxesFilled(addMoviePersonMainGrid))
                {
                    throw new Exception("not all text boxes filled!");
                }
                myGender mg = checkMaleOrFemale();
                bool dir = cbDirector.IsChecked == true;
                bool act = cbActor.IsChecked == true;

                newPerson = new MoviePerson(tbFirstName.Text, tbLastName.Text, mg, tbBirthDate.Text, dir, act);
                MessageBox.Show($"{newPerson.FirstName} {newPerson.LastName} added!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private myGender checkMaleOrFemale()
        {
            if (cbMale.IsChecked == true) return myGender.male;
            else return myGender.female;
        }

        private bool checkAllBoxesFilled(Panel p)
        {
            if(string.IsNullOrEmpty(tbFirstName.Text) ||
                string.IsNullOrEmpty(tbLastName.Text) ||
                string.IsNullOrEmpty(tbBirthDate.Text))
            {
                throw new Exception("All text box must be filled!");
            }
            if (cbMale.IsChecked == false && cbFemale.IsChecked == false)
            {
                throw new Exception("Gender must be checked");
            }
            if (cbActor.IsChecked == false && cbDirector.IsChecked == false)
            {
                throw new Exception("New Person must have role");
            }
            return true;
        }

        private void cbMale_Checked(object sender, RoutedEventArgs e)
        {
            cbFemale.IsEnabled = false;
        }

        private void cbFemale_Checked(object sender, RoutedEventArgs e)
        {
            cbMale.IsEnabled = false;
        }

        private void cbMale_Unchecked(object sender, RoutedEventArgs e)
        {
            cbFemale.IsEnabled = true;
        }

        private void cbFemale_Unchecked(object sender, RoutedEventArgs e)
        {
            cbMale.IsEnabled = true;
        }

        private void btnAddMoviePerson_Click(object sender, RoutedEventArgs e)
        {
            bool ret = generatePerson();
            if (ret == true) cleanForm(addMoviePersonMainGrid);
        }

        private void cleanForm(Panel p)
        {
            foreach (var item in p.Children)
            {
                if (item is TextBox)
                {
                    TextBox tb = item as TextBox;
                    if (tb.IsEnabled == true && !string.IsNullOrEmpty(tb.Text))
                    {
                        tb.Text = "";
                    }
                }
            }
            if (cbMale.IsChecked == true)
            {
                cbMale.IsChecked = false;
            }
            if (cbFemale.IsChecked == true)
            {
                cbFemale.IsChecked = false;
            }
        }
    }
}
