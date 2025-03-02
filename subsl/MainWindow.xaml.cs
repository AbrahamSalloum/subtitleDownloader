using System.Collections.ObjectModel;
using System.Windows;
using subsl.Services;
using subsl.Models;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.ComponentModel;


namespace subsl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public ObservableCollection<ItemList> Subtitles { get; set; }
        public ObservableCollection<FeatureType> FeatureTypes { get; set; }
        public ObservableCollection<Langdef> Langauges { get; set; }

        public ObservableCollection<YearType> Years { get; set; }

        private ItemList CurrentSelected;
        private OpenSubtitlesAPI subs;
        private bool LoggedIn = false;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            Subtitles = new ObservableCollection<ItemList>();
            FeatureTypes = new ObservableCollection<FeatureType>(SearchInput.FeatureList as List<FeatureType>);
            Langauges = new ObservableCollection<Langdef>(SearchInput.LangList as List<Langdef>);
            Years = new ObservableCollection<YearType>(SearchInput.Generateyears() as List<YearType>);
            CurrentSelected = new ItemList();
            subs = new OpenSubtitlesAPI();


        }

        private void SearchText(object sender, RoutedEventArgs e)
        {

            if (query != "")
            {
                SearchInput.AddQuery("type", feat);
                SearchInput.AddQuery("languages", lang);
                SearchInput.AddQuery("query", query);
                SearchInput.AddQuery("year", year);
                hash = null;
                SearchSubtitle();
            }
        }
        private async void SearchSubtitle()
        {

            if (subs != null)
            {
                StatusTxt = "Searching...";
                SearchResults SubtitleSearchResults = await subs.Search(SearchInput.Query);

                if (SubtitleSearchResults?.data != null)
                {
                    Subtitles.Clear();
                    foreach (var item in SubtitleSearchResults.data)
                    {
                        Subtitles.Add(item);
                    }
                }
                else
                {
                    StatusTxt = "No Results Found.";
                    return;
                }
            }

            StatusTxt = null;
        }
        private void ListViewItem_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var listView = sender as ListView;
            CurrentSelected = listView?.SelectedItem as ItemList;
            Debug.WriteLine(CurrentSelected?.attributes?.url);
            MessageBox.Show("Nothing.");

        }
        private void ListViewItem_MouseClick(object sender, System.Windows.Input.MouseButtonEventArgs? e)
        {
            var listView = sender as ListView;
            CurrentSelected = listView?.SelectedItem as ItemList;
            PosterStatus = null;
            img = null;
            if (CurrentSelected?.attributes?.related_links?[0]?.img_url != null)
            {
                PosterStatus = "Loading Poster.";
                string? imgurl = CurrentSelected.attributes.related_links[0].img_url;
                if (imgurl != null)
                {
                    img = new BitmapImage(new Uri(imgurl));
                }

            }
            else
            {
                PosterStatus = "No Poster Found.";
            }
            movieTitle = null;
            if (CurrentSelected?.attributes?.feature_details?.title != null)
            {
                movieTitle = CurrentSelected.attributes.feature_details.title;
            }

            movieimdb_id = null;
            if (CurrentSelected?.attributes?.feature_details?.imdb_id != null)
            {
                movieimdb_id = CurrentSelected.attributes.feature_details.imdb_id.ToString();
            }

            movieyear = null;
            if (CurrentSelected?.attributes?.feature_details?.year != null)
            {
                movieyear = CurrentSelected.attributes.feature_details.year.ToString();
            }

        }
        private async void DownLoadSub_Click(object sender, RoutedEventArgs e)
        {

            DownloadLinkInfo? dlinfo;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "srt";
            saveFileDialog.Filter = "Subtitles (*.srt)|*.srt|All files (*.*)|*.*";



            if (CurrentSelected?.attributes?.subtitle_id != null)
            {

                StatusTxt = $"Downloading {CurrentSelected?.attributes?.subtitle_id}.";
                dlinfo = await subs.RequestDownloadInfo(CurrentSelected?.attributes?.files[0].file_id);
                if (dlinfo != null)
                {
                    saveFileDialog.FileName = $"{dlinfo.file_name}.{CurrentSelected?.attributes?.subtitle_id}.srt";
                }
                else
                {
                    MessageBox.Show("No Download Link Found.");
                    return;
                }
            }
            else
            {

                MessageBox.Show("No Subtitle Selected.");
                return;
            }

            if (dlinfo.message == "error")
            {
                LoginWindow login = new LoginWindow();
                login.ShowDialog();
                await subs.Login();
                return;
            }

            if (saveFileDialog.ShowDialog() == true)
            {

                await subs.DownloadSubtitle(dlinfo.link, saveFileDialog.FileName);
                StatusTxt = "";
            }
        }
        private void OpenOptionsWindow(object sender, RoutedEventArgs e)
        {
            OptionsWindow options = new OptionsWindow();
            options.Show();
        }
        private void SearchMovieHash(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                string filename = openFileDialog.FileName;
                byte[] moviehash = MovieHash.ComputeMovieHash($"{filename}");
                hash = MovieHash.ToHexadecimal(moviehash);

                if (hash != "")
                {
                    SearchInput.AddQuery("moviehash", hash);
                    SearchSubtitle();
                    SearchInput.RemoveQuery("moviehash");

                }
            }
        }



        public event PropertyChangedEventHandler? PropertyChanged;


        private string? _lang = "";
        public string? lang
        {
            get { return _lang; }
            set
            {
                if (_lang != value)
                {
                    _lang = value;
                    NotifyPropertyChanged(nameof(lang));
                }
            }
        }


        private string? _StatusTxt;
        public string? StatusTxt
        {
            get { return _StatusTxt; }
            set
            {
                if (_StatusTxt != value)
                {
                    _StatusTxt = value;
                    NotifyPropertyChanged(nameof(StatusTxt));
                }
            }
        }


        private string? _query;
        public string? query
        {
            get { return _query; }
            set
            {
                if (_query != value)
                {
                    _query = value;
                    NotifyPropertyChanged(nameof(query));
                }
            }
        }

        private string? _feat = "all";
        public string? feat
        {
            get { return _feat; }
            set
            {
                if (_feat != value)
                {
                    _feat = value;
                    NotifyPropertyChanged(nameof(feat));
                }
            }
        }


        private string? _hash;
        public string? hash
        {
            get { return _hash; }
            set
            {
                if (_hash != value)
                {
                    _hash = value;
                    NotifyPropertyChanged(nameof(hash));
                }
            }
        }

        private string? _PosterStatus;
        public string? PosterStatus
        {
            get { return _PosterStatus; }
            set
            {
                if (_PosterStatus != value)
                {
                    _PosterStatus = value;
                    NotifyPropertyChanged(nameof(PosterStatus));
                }
            }
        }

        private string? _movieyear;
        public string? movieyear
        {
            get { return _movieyear; }
            set
            {
                if (_movieyear != value)
                {
                    _movieyear = value;
                    NotifyPropertyChanged(nameof(movieyear));
                }
            }
        }



        private string? _movieimdb_id;
        public string? movieimdb_id
        {
            get { return _movieimdb_id; }
            set
            {
                if (_movieimdb_id != value)
                {
                    _movieimdb_id = value;
                    NotifyPropertyChanged(nameof(movieimdb_id));
                }
            }
        }


        private string? _movieTitle;
        public string? movieTitle
        {
            get { return _movieTitle; }
            set
            {
                if (_movieTitle != value)
                {
                    _movieTitle = value;
                    NotifyPropertyChanged(nameof(movieTitle));
                }
            }
        }

        private string? _year = null;
        public string? year
        {
            get { return _year; }
            set
            {
                if (_year != value)
                {
                    _year = value;
                    NotifyPropertyChanged(nameof(year));
                }
            }
        }


        private BitmapImage? _img;
        public BitmapImage? img
        {
            get { return _img; }
            set
            {
                if (_img != value)
                {
                    _img = value;
                    NotifyPropertyChanged(nameof(img));
                }
            }
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}