using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml;
using MovieProjectClasses;

namespace MovieApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private static string filePath = "Data.xml";
        private static string fileData = "appData.xml";

        ObservableCollection<MoviePerson> movieDirectors;
        ObservableCollection<MoviePerson> movieActors;
        ObservableDictionary<MyKeyPair, Movie> movies;
        public MainWindow()
        {
            InitializeComponent();
        }

        private delegate void connectObservableCollections(ObservableCollection<MoviePerson> dir, ObservableCollection<MoviePerson> act, ObservableDictionary<MyKeyPair, Movie> mov);
        private event connectObservableCollections sendToWindow;
        #region helpers
        /// <summary>
        /// order all lists
        /// </summary>
        void orderAllLists()
        {
            movieActors.OrderBy(act => act.FirstName).ThenBy(act => act.LastName);
            movieDirectors.OrderBy(dir => dir.FirstName).ThenBy(dir => dir.LastName);
            movies.OrderDict();
        }

        /// <summary>
        /// Save application data at output XML file
        /// </summary>
        private void saveDataToFile()
        {
            //Write movie directors
            XmlTextWriter writer = new XmlTextWriter(fileData, Encoding.Unicode)
            {
                Formatting = Formatting.Indented
            };
            writer.WriteStartDocument();
            writer.WriteStartElement("DATA");
            writer.WriteStartElement("MovieDirectors");
            foreach (var md in movieDirectors)
            {
                writer.WriteStartElement("MovieDirector");
                writer.WriteElementString("FN", md.FirstName);
                writer.WriteElementString("LN", md.LastName);
                writer.WriteElementString("GEN", md.Gender.ToString());
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
                writer.WriteElementString("GEN", ma.Gender.ToString());
                writer.WriteElementString("BD", ma.BirthDate.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            //Write movies
            if (movies.Dict.Count != 0)
            {
                writer.WriteStartElement("Movies");
                foreach (var m in movies.Dict)
                {
                    writer.WriteStartElement("Movie");
                    writer.WriteElementString("Title", m.Value.title);
                    writer.WriteElementString("Dir", m.Value.Director.ToString());
                    writer.WriteElementString("Year", m.Value.Year.ToString());
                    writer.WriteElementString("RTS", m.Value.RotTomScore.ToString());
                    writer.WriteElementString("IS", m.Value.ImdbScore.ToString());
                    foreach (var s in m.Value.Actors)
                    {
                        writer.WriteElementString("Actor", s);
                    }
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();

            }
            writer.WriteEndDocument();
            writer.Close();
        }

        /// <summary>
        /// Handle load data from file at application initialization process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <summary>
        /// Get data from XML file
        /// </summary>
        void readDataFromXml()
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
                string gen = reader.ReadElementString("GEN");
                string bd = reader.ReadElementString("BD");
                MoviePerson.myGender g;
                if (gen.Equals("male"))
                    g = MoviePerson.myGender.male;
                else
                    g = MoviePerson.myGender.female;
                a = new MoviePerson(fn, ln, g, bd, true, false);
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
                a = new MoviePerson(fn, ln, g, bd, false, true);
                movieActors.Add(a);
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
                MoviePerson realDirector = findMoviePersonByName(dir);
                int year = int.Parse(reader.ReadElementString("Year"));
                int RT = int.Parse(reader.ReadElementString("RTS"));
                decimal IS = decimal.Parse(reader.ReadElementString("IS"));
                List<string> actors = new List<string>();
                while (reader.Name == "Actor")
                {
                    actors.Add(reader.ReadElementString("Actor"));
                }

                Movie m = new Movie(title, realDirector, year, RT, IS, actors);
                movies.Add(new MyKeyPair(title, year), m);
                reader.ReadEndElement();
            }

            reader.Close();
        }

        /// <summary>
        /// search for director in moviedirectors
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        MoviePerson findMoviePersonByName(string s)
        {
            string[] split = s.Split();
            foreach (var n in movieDirectors)
                if (split[0].Equals(n.FirstName) && split[1].Equals(n.LastName))
                    return n;
            return null;
        }
        #endregion

        #region menu_clicks
        private void add_Person_click(object sender, RoutedEventArgs e)
        {
            AddMoviePersonWindow addActorWindow = new AddMoviePersonWindow();
            sendToWindow += addActorWindow.connecListBox;
            sendToWindow(movieDirectors, movieActors, movies);
        }

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

        private void search_by_imdb_click(object sender, RoutedEventArgs e)
        {
            if (movies.Dict.Count == 0)
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
            if (movies.Dict.Count == 0)
            {
                MessageBox.Show("No movies entered", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            searchStringWindow sw = new searchStringWindow("name", movies);
        }

        private void search_by_year_click(object sender, RoutedEventArgs e)
        {
            if (movies.Dict.Count == 0)
            {
                MessageBox.Show("No movies entered", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            searchStringWindow sw = new searchStringWindow("year", movies);
        }

        private void search_by_director_click(object sender, RoutedEventArgs e)
        {
            if (movies.Dict.Count == 0)
            {
                MessageBox.Show("No movies entered", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            searchStringWindow sw = new searchStringWindow("director", movies);
        }

        private void search_by_actor_click(object sender, RoutedEventArgs e)
        {
            if (movies.Dict.Count == 0)
            {
                MessageBox.Show("No movies entered", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            searchStringWindow sw = new searchStringWindow("actor", movies);
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if (movies.Dict.Count == 0)
            {
                MessageBox.Show("No movies entered yet", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (lbMovies.SelectedItem == null)
            {
                MessageBox.Show("Must pick movie to delete", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var selDel = lbMovies.SelectedItem.ToString();
            var stL = selDel.Split(',');
            string title = stL[1].Trim();
            stL[2] = stL[2].Substring(0, stL[2].Length);
            int year = int.Parse(stL[2].Trim());
            MyKeyPair keyDel = new MyKeyPair(title, year);
            MessageBoxResult res = MessageBox.Show($"Are you sure you want to delete '{title}'?", "Confirm pick", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.No)
            {
                return;
            }
            foreach (var mov in movies.Dict)
            {
                if (mov.Key.Equals(keyDel))
                {
                    keyDel = mov.Key;
                }
            }
            movies.Remove(keyDel);
            movies.OrderDict();
            return;
        }
        #endregion

        #region save_load
        private void Window_Initialized(object sender, EventArgs e)
        {
            movieActors = new ObservableCollection<MoviePerson>();
            movieDirectors = new ObservableCollection<MoviePerson>();
            movies = new ObservableDictionary<MyKeyPair, Movie>();
            DataContext = movies;
            orderAllLists();
            if (File.Exists(filePath))
            {
                using (StreamReader handle = new StreamReader(filePath))
                {
                    fileData = handle.ReadToEnd();
                }
                if (File.Exists(fileData))
                {
                    readDataFromXml();
                }
            }
        }

        /// <summary>
        /// Handle application closing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (movieActors.Count > 0 || movieDirectors.Count > 0)
            {
                using (StreamWriter handle = new StreamWriter(filePath))
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
        #endregion
    }
}
