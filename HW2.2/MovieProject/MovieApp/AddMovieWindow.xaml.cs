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
using MovieProjectClasses;

namespace MovieApp
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


        private const int NOTFOUND = -1;

        #region buttons_fanctionality
        /// <summary>
        /// Handle add book button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!AllTextBoxesFilled())
                {
                    MessageBox.Show("You must fill all the data");
                    return;
                }
                GenerateEmployee();
                this.Close();
            }
            catch (ValidationException ve)
            {
                MessageBox.Show(ve.Message);
            }
            catch (Exception ve)
            {
                MessageBox.Show(ve.Message);
            }
        }

        private void GenerateEmployee()
        {
            NewEmployee = new Employee()
            {
                FirstName = tbFirstName.Text.Trim(),
                LastName = tbLastName.Text.Trim(),
                Id = tbId.Text.Trim(),
                StreetAddress = tbAddress.Text.Trim(),
                City = tbCity.Text.Trim(),
                ZipCode = tbZip.Text.Trim(),
                Phone = tbPhone.Text.Trim(),
                Email = tbEmail.Text.Trim()
            };
        }
            catch (WrongIsbnException wie)
            {
                MessageBox.Show(wie.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (ArgumentException ae)
            {
                MessageBox.Show(ae.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        #region side_Functions
        /// <summary>
        /// check if all text boxes inside given Panel are filled
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>

        private bool allTextBoxFilled(Panel p)
{
    foreach (var item in p.Children)
    {
        if (item is TextBox)
        {
            TextBox tb = item as TextBox;
            if (tb.IsEnabled == true && string.IsNullOrEmpty(tb.Text))
            {
                return false;
            }
        }
    }
    return true;
}
/// <summary>
/// clear all text boxes inside given Panel
/// </summary>
/// <param name="p"></param>
private void clearBooksTextBoxes(Panel p)
{
    foreach (var item in p.Children)
    {
        if (item is TextBox)
        {
            TextBox tb = item as TextBox;
            tb.Text = null;
        }
    }
}


        #endregion side_Functions
    }
}