using MovieProjectClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<MoviePerson> directors;
        public ObservableCollection<MoviePerson> actors;
        public AddMoviePersonWindow()
        {
            InitializeComponent();
            Show();
        }

        #region helpers

        /// <summary>
        /// create new person from user input
        /// </summary>
        /// <returns></returns>
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
                MoviePerson temp = new MoviePerson(tbFirstName.Text, tbLastName.Text, mg, tbBirthDate.Text, dir, act);
                if (directors.Contains(temp) || actors.Contains(temp))
                {
                    throw new Exception($"{temp.FirstName} {temp.LastName} added before");
                }
                if (dir == true)
                {
                    directors.Add(temp);
                }
                if (act == true)
                {
                    actors.Add(temp);
                }

                return true;
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        /// <summary>
        /// connect mainwindow's lists with addmoviepersonwindow's lists 
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="act"></param>
        /// <param name="mov"></param>
        public void connecListBox(ObservableCollection<MoviePerson> dir, ObservableCollection<MoviePerson> act, ObservableDictionary<MyKeyPair, Movie> mov)
        {
            actors = act;
            directors = dir;
        }

        /// <summary>
        /// gender check
        /// </summary>
        /// <returns></returns>
        private myGender checkMaleOrFemale()
        {
            if (cbMale.IsChecked == true) return myGender.male;
            else return myGender.female;
        }

        /// <summary>
        /// check all tb in XAML filled
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
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

        /// <summary>
        /// empty all TB
        /// </summary>
        /// <param name="p"></param>
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
            if (cbActor.IsChecked == true)
            {
                cbActor.IsChecked = false;
            }
            if (cbDirector.IsChecked == true)
            {
                cbDirector.IsChecked = false;
            }
        }
        #endregion

        #region CB_checks

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
        #endregion

        #region clicks
        private void btnAddMoviePerson_Click(object sender, RoutedEventArgs e)
        {
            bool ret = generatePerson();
            if (ret == true) cleanForm(addMoviePersonMainGrid);
        }
        #endregion
    }
}
