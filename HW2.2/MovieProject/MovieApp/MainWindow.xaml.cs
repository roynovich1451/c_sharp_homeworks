using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Xml;
using Microsoft.VisualBasic.FileIO;
using MovieProjectClasses;

namespace MovieApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private static string fileData = "appData.xml";

        ObservableCollection<MoviePerson> movieDirectors;
        ObservableCollection<MoviePerson> movieActors;
        ObservableDictionary<MyKeyPair, Movie> movies;
        public MainWindow()
        {
            InitializeComponent();
            movieActors = new ObservableCollection<MoviePerson>();
            movieDirectors = new ObservableCollection<MoviePerson>();
            movies = new ObservableDictionary<MyKeyPair, Movie>();
            DataContext = movies;
            orderAllLists();
        }

        private delegate void connectObservableCollections(ObservableCollection<MoviePerson> dir, ObservableCollection<MoviePerson> act, ObservableDictionary<MyKeyPair, Movie> mov);
        private event connectObservableCollections sendToWindow;

        #region menu_clicks
        private void add_Person_click(object sender, RoutedEventArgs e)
        {
            AddMoviePersonWindow addActorWindow = new AddMoviePersonWindow();
            sendToWindow += addActorWindow.connecListBox;
            sendToWindow(movieDirectors, movieActors, movies);
        }
        #endregion

        private void add_movie_click(object sender, RoutedEventArgs e)
        {
            if (movieActors.Count == 0 || movieDirectors.Count == 0)
            {
                MessageBox.Show("Before adding movie, need actors and directors", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            AddMovieWindow addMovieWindow = new AddMovieWindow();
            sendToWindow += addMovieWindow.connecListBox;
            sendToWindow(movieDirectors, movieActors, movies);
        }
        void orderAllLists()
        {
            movieActors.OrderBy(act => act.FirstName).ThenBy(act => act.LastName);
            movieDirectors.OrderBy(dir => dir.FirstName).ThenBy(dir => dir.LastName);
            movies.OrderDict();
        } 

        private void search_by_imdb_click(object sender, RoutedEventArgs e)
        {
            if(movies.Dict.Count == 0)
            {
                MessageBox.Show("No movies entered", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            SearchWindow sw = new SearchWindow("IMDB", movies);
        }

        private void search_by_rotten_click(object sender, RoutedEventArgs e)
        {
            if (movies.Dict.Count == 0)
            {
                MessageBox.Show("No movies entered", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            SearchWindow sw = new SearchWindow("Rotten", movies);
        }

        private void search_by_name_click(object sender, RoutedEventArgs e)
        {

        }

        private void search_by_year_click(object sender, RoutedEventArgs e)
        {

        }

        private void search_by_director_click(object sender, RoutedEventArgs e)
        {

        }

        private void search_by_actor_click(object sender, RoutedEventArgs e)
        {

        }

        /*
                private void Window_Initialized(object sender, EventArgs e)
                {

                    /*if (File.Exists("filePath"))
                    {
                        using (StreamReader handle = new StreamReader("filePath"))
                        {
                            fileData = handle.ReadToEnd();
                        }
                        if (File.Exists(fileData))
                        {
                            Console.WriteLine("exist");
                            readDataFromXml();
                        }
                    }

                }
                 */
        /// <summary>
        /// Handle application closing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /*
                private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
                {
                    if (movieActors.Count > 0 || movieDirectors.Count > 0)
                    {
                        using (StreamWriter handle = new StreamWriter("filePath"))
                        {
                            handle.Write(fileData);
                        }
                        saveDataToFile();
                    }
                    else
                    {
                        File.Delete(fileData);
                    }
                }
                */
        /// <summary>
        /// Save application data at output XML file
        /// </summary>
        /*
        private void saveDataToFile()
        {
            //Write movie directors
            XmlTextWriter writer = new XmlTextWriter(fileData, Encoding.Unicode);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            writer.WriteStartElement("DATA");
            writer.WriteStartElement("MovieDirectors");
            foreach (var md in movieDirectors)
            {
                writer.WriteStartElement("MovieDirector");
                writer.WriteElementString("FN", md.FirstName);
                writer.WriteElementString("LN", md.LastName);
                writer.WriteElementString("Gen", md.Gender.ToString());
                writer.WriteElementString("BD", md.BirthDate.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            //Write movie actors
            writer.WriteStartElement("MovieActors");
            foreach (var ma in movieActors)
            {
                writer.WriteStartElement("MovieActor");
                writer.WriteElementString("FN", ma.FirstName);
                writer.WriteElementString("LN", ma.LastName);
                writer.WriteElementString("Gen", ma.Gender.ToString());
                writer.WriteElementString("BD", ma.BirthDate.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            //Write movies
            writer.WriteStartElement("Movies");
            foreach (var m in movies)
            {
                writer.WriteStartElement("Movie");
                writer.WriteElementString("Title", m.Name);
                writer.WriteElementString("Dir", m.director);
                writer.WriteElementString("Year", m.year);
                writer.WriteElementString("RTS", m.rotTomScore.ToString());
                writer.WriteElementString("IS", m.imdbScore.ToString());
                writer.WriteString("Actors:");
                int i = 0;
                foreach (var s in m.actors)
                {
                    writer.WriteElementString("Actors:", i + ") " + s);
                    i++;
                }
                writer.WriteEndElement();
            
        writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }
  */
        /// <summary>
        /// Handle load data from file at application initialization process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        /// <summary>
        /// Get data from XML file
        /// </summary>
        /*
        private void readDataFromXml()
        {
            XmlTextReader reader = new XmlTextReader(fileData);
            reader.WhitespaceHandling = WhitespaceHandling.None;
            while (reader.Name != "DATA")
            {
                reader.Read();
            }

            //Read movie directors
            while (reader.Name != "MovieDirectors")
            {
                reader.Read();
            }
            reader.Read();
            while (reader.Name == "MovieDirector")
            {
                MoviePerson a;

                reader.ReadStartElement("MovieDirector");
                string fn = reader.ReadElementString("FN");
                string ln = reader.ReadElementString("LN");
                //TODO:
                //gen line throw "System.Xml.XmlException: 'Element 'GEN' was not found. Line 7, position 8.'" need to fix
                string gen = reader.ReadElementString("GEN");
                string bd = reader.ReadElementString("BD");
                MoviePerson.myGender g;
                if (gen.Equals("male"))
                    g = MoviePerson.myGender.male;
                else
                    g = MoviePerson.myGender.female;
                a = new MoviePerson(fn, ln, g, bd, false, true);
                movieDirectors.Add(a);
                reader.ReadEndElement();
            }

            //Read movie actors

            while (reader.Name != "MovieActors")
            {
                reader.Read();
            }
            reader.Read();
            while (reader.Name == "MovieActor")
            {
                MoviePerson a;

                reader.ReadStartElement("MovieActor");
                string fn = reader.ReadElementString("FN");
                string ln = reader.ReadElementString("LN");
                string gen = reader.ReadElementString("GEN");
                MoviePerson.myGender g;
                if (gen.Equals("male"))
                    g = MoviePerson.myGender.male;
                else
                    g = MoviePerson.myGender.female;
                string bd = reader.ReadElementString("BD");
                a = new MoviePerson(fn, ln, g, bd, true, false);
                movieDirectors.Add(a);
                reader.ReadEndElement();
            }


            //Read movies
            while (reader.Name != "Movies")
            {
                reader.Read();
            }
            reader.Read();
            while (reader.Name == "Movie")
            {

                reader.ReadStartElement("Movie");

                string title = reader.ReadElementString("Title");
                string dir = reader.ReadElementString("Dir");
                int year = int.Parse(reader.ReadElementString("Year"));
                int RT = int.Parse(reader.ReadElementString("RT"));
                decimal IS = decimal.Parse(reader.ReadElementString("IS"));
                //TODO:
                //first null need to be MoviePerson from directors list, need to search correct instance
                //at directors list
                //second null need to be List<string> with the actors name participate in the movie.
                Movie m = new Movie(title, null, year, RT, IS, null);
                // Movies.Add(b);
                reader.ReadEndElement();
            }
            reader.Close();
        }
        */


    }
}
