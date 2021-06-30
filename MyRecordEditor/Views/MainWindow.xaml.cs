using MyRecordEditor.DataAccess;
using MyRecordEditor.Model;
using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MyRecordEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields
        private int _idsong;
        private int _index = 0;
        private int _idgenre;
        private int _idcatalog;
        private int _idmedia;
        private bool IsSongChanged = false;
        private tSongsModel songrecord;
        private ArrayList _ids = new ArrayList();

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
            FillComboMedias();
        }
        #endregion

        #region Control Events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string[] p = Environment.GetCommandLineArgs();

            if (p.Length > 1)
            {
                _ids.AddRange(p);
                _ids.RemoveAt(0);
                _idsong = Convert.ToInt32(_ids[_index]);
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

        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            if (_index < _ids.Count - 1)
            {
                _index++;
                LoadData(Convert.ToInt32(_ids[_index]));
                statusbarSavedContent.Visibility = Visibility.Hidden;
                statusSavedGreen.Visibility = Visibility.Hidden;
                statusSavedRed.Visibility = Visibility.Hidden;
                ID = Convert.ToInt32(textboxIDInput.Text);
            }
        }

        private void buttonPrev_Click(object sender, RoutedEventArgs e)
        {
            if (_index > 0)
            {
                _index--;
                LoadData(Convert.ToInt32(_ids[_index]));
                statusbarSavedContent.Visibility = Visibility.Hidden;
                statusSavedGreen.Visibility = Visibility.Hidden;
                statusSavedRed.Visibility = Visibility.Hidden;
                ID = Convert.ToInt32(textboxIDInput.Text);
            }
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

        private void comboboxGenres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tGenresModel genre = (tGenresModel)comboboxGenres.SelectedItem;
            _idgenre = genre.ID;
            songrecord.ID_Genre = _idgenre;
            IsSongChanged = true;
        }

        private void comboboxCatalogs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tCatalogsModel catalog = (tCatalogsModel)comboboxCatalogs.SelectedItem;
            _idcatalog = catalog.ID;
            songrecord.ID_Catalog = _idcatalog;
            IsSongChanged = true;
        }

        private void comboboxMediass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tMediasModel media = (tMediasModel)comboboxMedias.SelectedItem;
            _idmedia = media.ID;
            songrecord.ID_Media = _idmedia;
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
                songrecord.Artist = textboxArtist.Text;
        }

        private void textboxAlbum_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IsSongChanged)
                songrecord.Album = textboxAlbum.Text;
        }

        private void textboxTitle_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IsSongChanged)
                songrecord.Title = textboxTitle.Text;
        }

        private void textboxPath_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IsSongChanged)
                songrecord.Path = textboxPath.Text;
        }

        private void textboxFilename_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IsSongChanged)
                songrecord.FileName = textboxFilename.Text;
        }
        #endregion

        #region Methods
        private void FillComboGenres()
        {
            var genres = DataAccess.GetSetData.GetGenres();
            comboboxGenres.ItemsSource = genres;
        }

        private void FillComboCatalogs()
        {
            var catalogs = GetSetData.GetCatalogs();
            comboboxCatalogs.ItemsSource = catalogs;
        }

        private void FillComboMedias()
        {
            var medias = GetSetData.GetMedias();
            comboboxMedias.ItemsSource = medias;
        }

        private int ListGenreIndex(int idgenre)
        {
            int index = -1;
            foreach (tGenresModel item in comboboxGenres.Items)
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
            foreach (tCatalogsModel item in comboboxCatalogs.Items)
            {
                index++;
                if (item.ID == _idcatalog)
                    break;
            }

            return index;
        }

        private int ListMediaIndex(int idmedia)
        {
            int index = -1;
            foreach (tMediasModel item in comboboxMedias.Items)
            {
                index++;
                if (item.ID == _idmedia)
                    break;
            }

            return index;
        }

        //private int GetIndex(int id)
        //{
        //    ObservableCollection<tMediasModel> items = new ObservableCollection<tMediasModel>(comboboxMedias.ItemsSource);

        //    items.Any(i => i.Contains(id));


        //}


        //private void LoadData(int ID)
        //{
        //    mp3Record = new MP3Record();
        //    mp3Record = DataGetSet.GetRecord(ID);

        //    this.DataContext = mp3Record;

        //    if (mp3Record != null)
        //    {
        //        textboxIDInput.Text = ID.ToString();
        //        statusbarID.Content = ID.ToString();

        //        textboxAlbum.Text = mp3Record.Album;
        //        textboxArtist.Text = mp3Record.Artist;
        //        textboxTitle.Text = mp3Record.Titel;
        //        textboxPath.Text = mp3Record.Path;
        //        textboxFilename.Text = mp3Record.FileName;

        //        _idgenre = mp3Record.ID_Genre;
        //        int index = ListGenreIndex(_idgenre);
        //        comboboxGenres.SelectedIndex = index;
        //        comboboxGenres.SelectedItem = index;

        //        _idcatalog = mp3Record.ID_Catalog;
        //        index = ListCatalogIndex(_idcatalog);
        //        comboboxCatalogs.SelectedIndex = index;
        //        mp3Record.Initialize();
        //    }
        //    else
        //    {
        //        MessageBox.Show($"Record ID {ID} not found!", "ERROR: Load Data!");
        //    }
        //    IsSongChanged = false;
        //    statusbarChanged.Visibility = Visibility.Hidden;
        //}

        private void LoadData(int id)
        {
            int index = -1;

            songrecord = new tSongsModel();

            songrecord = GetSetData.GetSongModel(id);
            this.DataContext = songrecord;

            _idgenre = songrecord.ID_Genre;
            index = ListGenreIndex(_idgenre);
            comboboxGenres.SelectedIndex = index;
            comboboxGenres.SelectedItem = index;

            _idcatalog = songrecord.ID_Catalog;
            index = ListCatalogIndex(_idcatalog);
            comboboxCatalogs.SelectedIndex = index;

            _idmedia = songrecord.ID_Media;
            index = ListMediaIndex(_idmedia);
            comboboxMedias.SelectedIndex = index;

        }

        private void SaveData()
        {
            if (IsSongChanged == false)
            {
                MessageBox.Show("No changes made.", "INFO: SaveData");
                return;
            }

            tGenresModel genre = (tGenresModel)comboboxGenres.SelectedItem;

            bool result = GetSetData.SaveChanges(songrecord);

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
