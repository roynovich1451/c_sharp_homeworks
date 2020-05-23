using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using MovieProjectClasses;

namespace MovieApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        public ObservableCollection<MoviePerson> MovieDirectors { get; set; }
        public ObservableCollection<MoviePerson> MovieActors { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            MovieActors = new ObservableCollection<MoviePerson>();
            MovieDirectors = new ObservableCollection<MoviePerson>();
        }
        #region menu_clicks
        private void add_Person_click(object sender, RoutedEventArgs e)
        {
            AddMoviePersonWindow addActorWindow = new AddMoviePersonWindow();
            addActorWindow.ShowDialog();
            addActorWindow.Close();
            if (addActorWindow.newPerson.IsActor == true)
            {
                MovieActors.Add(addActorWindow.newPerson);
            }
            if (addActorWindow.newPerson.IsDirector == true)
            {
                MovieDirectors.Add(addActorWindow.newPerson);
            }
        }
        #endregion

        private void add_movie_click(object sender, RoutedEventArgs e)
        {
            AddMovieWindow addMovieWindow = new AddMovieWindow();

        }
    }
}
