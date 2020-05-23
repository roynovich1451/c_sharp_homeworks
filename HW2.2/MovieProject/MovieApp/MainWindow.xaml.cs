@@ -1,6 +1,7 @@
﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
+using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
@ -13,6 +14,8 @@ using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
+using System.Xml;
+using Microsoft.VisualBasic.FileIO;
using MovieProjectClasses;

namespace MovieApp
@ -20,7 +23,7 @@ namespace MovieApp
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    

    public partial class MainWindow : Window
    {
        public ObservableCollection<MoviePerson> MovieDirectors { get; set; }
@ -28,8 +31,7 @@ namespace MovieApp
        public MainWindow()
        {
            InitializeComponent();
            MovieActors = new ObservableCollection<MoviePerson>();//dont need init here
            MovieDirectors = new ObservableCollection<MoviePerson>();//dont need init here

        }
        #region menu_clicks
        private void add_Person_click(object sender, RoutedEventArgs e)
@ -53,5 +55,211 @@ namespace MovieApp
            AddMovieWindow addMovieWindow = new AddMovieWindow();

        }

        private MoviePerson findDirectorByName(string name)
        {
            foreach (var d in MovieDirectors)
            {
                //if (d.FirstName)
            }
            return null;
        }

        #region windowFunc

        private void Window_Initialized(object sender, EventArgs e)
        {
            MovieActors = new ObservableCollection<MoviePerson>();
            MovieDirectors = new ObservableCollection<MoviePerson>();
            //lbMovies.ItemsSource = Movies;
            if (File.Exists("filePath"))
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

        private static string fileData = "appData.xml";
        /// <summary>
        /// Handle application closing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MovieActors.Count > 0 || MovieDirectors.Count > 0)
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
        /// <summary>
        /// Save application data at output XML file
        /// </summary>
        private void saveDataToFile()
        {
            //Write movie directors
            XmlTextWriter writer = new XmlTextWriter(fileData, Encoding.Unicode);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            writer.WriteStartElement("DATA");
            writer.WriteStartElement("MovieDirectors");
            foreach (var md in MovieDirectors)
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
            foreach (var ma in MovieActors)
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
            /*foreach (var m in Movies)
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
            writer.WriteEndDocument();*/
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
                string gen = reader.ReadElementString("GEN");
                string bd = reader.ReadElementString("BD");
                MoviePerson.myGender g;
                if (gen.Equals("male"))
                    g = MoviePerson.myGender.male;
                else
                    g = MoviePerson.myGender.female;
                a = new MoviePerson(fn, ln, g, bd, false, true);
                MovieDirectors.Add(a);
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
                a = new MoviePerson(fn, ln, gen, g, true, false);
                MovieDirectors.Add(a);
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

                Movie m = new Movie(title, dir, year, RT, IS);
                // Movies.Add(b);
                reader.ReadEndElement();
            }
            reader.Close();
        }

        #endregion windowFunc
    }
}