using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ManageMovies
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MI_Add_New_actor_Click(object sender, RoutedEventArgs e)
        {
            var addActorWindow = new AddActorWindow();
            addActorWindow.ShowDialog();
        }

        private void MI_Add_New_director_Click(object sender, RoutedEventArgs e)
        {
            var addDirectorWindow = new AddDirectorWindow();
            addDirectorWindow.ShowDialog();
        }

        private void MI_Add_New_movie_Click(object sender, RoutedEventArgs e)
        {
            var addMovieWindow = new AddMovieWindow();
            addMovieWindow.ShowDialog();
        }

        private void MI_Add_Actor_movie_participate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MI_Add_Direcor_movie_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MI_Add_Movie_cast_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MI_Movie_by_year_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MI_Actors_shared_movie_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MI_Oscar_winner_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MI_Movie_datails_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MI_Add_Oscar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
