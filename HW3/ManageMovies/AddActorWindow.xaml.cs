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
        # endregion CB_checks

        private void btnAddActor_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
