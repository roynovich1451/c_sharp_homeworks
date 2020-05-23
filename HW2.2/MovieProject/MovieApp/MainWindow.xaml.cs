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
        
        ObservableCollection<MoviePerson> movieDirectors;
        ObservableCollection<MoviePerson> movieActors;
        ObservableDictionary<MyKeyPair, Movie> movies;
        public MainWindow()
        {
            InitializeComponent();
            movieDirectors = new ObservableCollection<MoviePerson>();
            movieActors = new ObservableCollection<MoviePerson>();
            movies = new ObservableDictionary<MyKeyPair, Movie>();
        }

        private delegate void connectObservableCollections(ObservableCollection<MoviePerson> dir, ObservableCollection<MoviePerson> act, ObservableDictionary<MyKeyPair, Movie> mov);
        private event connectObservableCollections sendToWindow;

        #region menu_clicks
        private void add_Person_click(object sender, RoutedEventArgs e)
        {
            AddMoviePersonWindow addActorWindow = new AddMoviePersonWindow();
            sendToWindow += addActorWindow.connecListBox;
            sendToWindow(movieDirectors, movieActors, movies);
            addActorWindow.ShowDialog();
            addActorWindow.Close();
        }
        #endregion

        private void add_movie_click(object sender, RoutedEventArgs e)
        {
            if(movieActors.Count == 0 || movieDirectors.Count == 0)
            {
                MessageBox.Show("Before adding movie, need actors and directors", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            AddMovieWindow addMovieWindow = new AddMovieWindow();
            sendToWindow += addMovieWindow.connecListBox;
            sendToWindow(movieDirectors, movieActors, movies);
            addMovieWindow.ShowDialog();
            addMovieWindow.Close();
        }
    }
}
