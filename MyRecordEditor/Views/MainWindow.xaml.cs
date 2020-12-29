using DbRecordEditor.BLL;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DbRecordEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields
        private int _idsong;
        private int _idgenre;
        private int _idcatalog;
        private bool IsSongChanged = false;
        private MP3Record mp3Record = new MP3Record();
        #endregion

        #region Properties

        public int ID
        {
            get { return _idsong; }
            set { _idsong = value; }
        }

        #endregion

        #region CTOR
        public MainWindow()
        {
            InitializeComponent();
            FillComboGenres();
            FillComboCatalogs();
        }
        #endregion

        #region Control Events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var p = Environment.GetCommandLineArgs();

            if (p.Length > 1)
            {
                ID = Convert.ToInt32(p[1]);
                LoadData(_idsong);
            }

            IsSongChanged = false;
        }

        private void Move_Window(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CommandMinimize_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void CommandClose_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void textboxIDInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    ID = Convert.ToInt32(textboxIDInput.Text);
                    LoadData(ID);
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "ERROR: ID Input");
                }
            }
        }

        private void comboboxGenres_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Genre genre = (Genre)comboboxGenres.SelectedItem;
            _idgenre = genre.ID;
            IsSongChanged = true;
        }

        private void comboboxCatalogs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Catalog catalog = (Catalog)comboboxCatalogs.SelectedItem;
            _idcatalog = catalog.ID;
            IsSongChanged = true;
        }

        private void CommandSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = IsSongChanged == true;
        }

        private void CommandSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveData();
        }

        private void textboxArtist_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IsSongChanged)
                mp3Record.Artist = textboxArtist.Text;
        }

        private void textboxAlbum_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IsSongChanged)
                mp3Record.Album = textboxAlbum.Text;
        }

        private void textboxTitle_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IsSongChanged)
                mp3Record.Titel = textboxTitle.Text;
        }

        private void textboxPath_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IsSongChanged)
                mp3Record.Path = textboxPath.Text;
        }

        private void textboxFilename_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IsSongChanged)
                mp3Record.FileName = textboxFilename.Text;
        }
        #endregion

        #region Methods
        private void FillComboGenres()
        {
            var genres = DataGetSet.GetGenres();
            comboboxGenres.ItemsSource = genres;
        }

        private void FillComboCatalogs()
        {
            var catalogs = DataGetSet.GetCatalogs();
            comboboxCatalogs.ItemsSource = catalogs;
        }

        private int ListGenreIndex(int idgenre)
        {
            int index = -1;
            foreach (Genre item in comboboxGenres.Items)
            {
                index++;
                if (item.ID == _idgenre)
                    break;
            }

            return index;
        }

        private int ListCatalogIndex(int idcatalog)
        {
            int index = -1;
            foreach (Catalog item in comboboxCatalogs.Items)
            {
                index++;
                if (item.ID == _idcatalog)
                    break;
            }

            return index;
        }

        private void LoadData(int ID)
        {
            mp3Record = new MP3Record();
            mp3Record = DataGetSet.GetRecord(ID);

            this.DataContext = mp3Record;

            if (mp3Record != null)
            {
                textboxIDInput.Text = ID.ToString();
                statusbarID.Content = ID.ToString();

                textboxAlbum.Text = mp3Record.Album;
                textboxArtist.Text = mp3Record.Artist;
                textboxTitle.Text = mp3Record.Titel;
                textboxPath.Text = mp3Record.Path;
                textboxFilename.Text = mp3Record.FileName;

                _idgenre = mp3Record.ID_Genre;
                int index = ListGenreIndex(_idgenre);
                comboboxGenres.SelectedIndex = index;

                _idcatalog = mp3Record.ID_Catalog;
                index = ListCatalogIndex(_idcatalog);
                comboboxCatalogs.SelectedIndex = index;
                mp3Record.Initialize();
            }
            else
            {
                MessageBox.Show($"Record ID {ID} not found!", "ERROR: Load Data!");
            }
            IsSongChanged = false;
            statusbarChanged.Visibility = Visibility.Hidden;
        }

        private void SaveData()
        {
            if (IsSongChanged == false)
            {
                MessageBox.Show("No changes made.", "INFO: SaveData");
                return;
            }

            mp3Record.Album = textboxAlbum.Text;
            mp3Record.Artist = textboxArtist.Text;
            mp3Record.Titel = textboxTitle.Text;
            mp3Record.Path = textboxPath.Text;
            mp3Record.FileName = textboxFilename.Text;

            Genre genre = (Genre)comboboxGenres.SelectedItem;
            mp3Record.ID_Genre = genre.ID;
            mp3Record.ID_Catalog = _idcatalog;

            bool result = DataGetSet.EditSaveSongChanges(ID, mp3Record);

            statusSavedGreen.Visibility = Visibility.Hidden;
            statusSavedRed.Visibility = Visibility.Hidden;

            if (result == true)
            {
                statusbarSavedContent.Visibility = Visibility.Visible;
                statusbarSaved.Visibility = Visibility.Visible;
                statusSavedGreen.Visibility = Visibility.Visible;
            }
            else
            {
                statusbarSavedContent.Visibility = Visibility.Visible;
                statusbarSaved.Visibility = Visibility.Visible;
                statusSavedRed.Visibility = Visibility.Visible;
            }

            LoadData(ID);
        }

        private void Record_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsSongChanged = true;
            statusSigne.Fill = Brushes.Red;
            statusbarChanged.Visibility = Visibility.Visible;
        }

        #endregion
    }
}
