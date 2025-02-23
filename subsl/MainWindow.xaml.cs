using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using subsl.Services;
using subsl.Models;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Security.Policy;

namespace subsl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public ObservableCollection<ItemList> Subtitles { get; set; }
        public ObservableCollection<String> FeatureType { get; set; }
        public ObservableCollection<String> Langauges { get; set; }

        private ItemList CurrentSelected;
        private OpenSubtitlesAPI subs;
        private bool LoggedIn = false;  
        public MainWindow()
        {
            InitializeComponent();
            
            this.DataContext = this;
            Subtitles = new ObservableCollection<ItemList>();
            FeatureType = new ObservableCollection<String>();
            Langauges = new ObservableCollection<String>();
            CurrentSelected = new ItemList();
            subs = new OpenSubtitlesAPI();
        }

        private void SearchText(object sender, RoutedEventArgs e)
        {
            string InputQueryText = searchBarInput.Text;

            if (InputQueryText != "")
            {


                SearchInput.Query["query"] = InputQueryText;
                MovieHashText.Text = "";
                SearchSubtitle();
            }
        }

        private async void SearchSubtitle()
        {

            if(LoggedIn == false)
            {
                StatusBox.Text = "Logging In...";
                await subs.Login();
                LoggedIn = true; 
            }
            

            if (subs != null)
            {
                StatusBox.Text = "Searching...";
                SearchResults SubtitleSearchResults = await subs.Search(SearchInput.Query);

                    if (SubtitleSearchResults?.data != null)
                    {
                        Subtitles.Clear();
                        foreach (var item in SubtitleSearchResults.data)
                        {
                            Subtitles.Add(item);
                            FeatureType.Add(item.attributes.feature_details.feature_type);
                            Langauges.Add(item.attributes.language);
                        }

                        sublist.ItemsSource = Subtitles;
                        FeatTypeCombo.ItemsSource = FeatureType.Distinct();
                        LangTypeCombo.ItemsSource = Langauges.Distinct();
                    }
                    else
                    {
                        StatusBox.Text = "No Results Found.";
                    return;
                    }
            }

            StatusBox.Text = "";
        }

        private void ListViewItem_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var listView = sender as ListView;
            CurrentSelected = listView?.SelectedItem as ItemList;
            Debug.WriteLine(CurrentSelected?.attributes?.url);
            MessageBox.Show("Nothing");
            
        }
        private void ListViewItem_MouseClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var listView = sender as ListView;
            CurrentSelected = listView?.SelectedItem as ItemList;
            if (CurrentSelected?.attributes?.related_links?[0]?.img_url != null)
            {
                PosterStatus.Text = "Loading Poster.";
                string imgurl = CurrentSelected.attributes.related_links[0].img_url;
                img.Source = new BitmapImage(new Uri(imgurl));
            } else
            {
                PosterStatus.Text = "No Poster Found.";
            }

            if (CurrentSelected?.attributes?.feature_details?.title != null)
            {
                movieTitle.Text = CurrentSelected.attributes.feature_details.title;
            }
                

            if (CurrentSelected?.attributes?.feature_details?.imdb_id != null)
            {
                movieimdb_id.Text = CurrentSelected.attributes.feature_details.imdb_id.ToString();
            }
                
            
            if (CurrentSelected?.attributes?.feature_details?.year != null)
            {
                movieyear.Text = CurrentSelected.attributes.feature_details.year.ToString();
            }
                
        }

        private async void DownLoadSub_Click(object sender, RoutedEventArgs e)
        {

            DownloadLinkInfo dlinfo;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "srt";
            saveFileDialog.Filter = "Subtitles (*.srt)|*.srt|All files (*.*)|*.*";
            


            if (CurrentSelected?.attributes?.subtitle_id != null)
            {
                StatusBox.Text = $"Downloading {CurrentSelected?.attributes?.subtitle_id}.";
                dlinfo = await subs.RequestDownloadInfo(CurrentSelected?.attributes?.subtitle_id);
                saveFileDialog.FileName = $"{dlinfo.file_name}.{CurrentSelected?.attributes?.subtitle_id}.srt";

            } else
            {

                MessageBox.Show("No Subtitle Selected.");
                return;
            
            }

            if (saveFileDialog.ShowDialog() == true)
            {
                
                await subs.DownloadSubtitle(dlinfo.link, saveFileDialog.FileName);
                StatusBox.Text = "";
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
                string hash = MovieHash.ToHexadecimal(moviehash);
                MovieHashText.Text = hash;

                if (hash != "")
                {
                    SearchInput.Query["moviehash"] = hash;
                    SearchInput.Query["moviehash_match"] = "only";

                    SearchSubtitle();

                    SearchInput.Query.Remove("moviehash");
                    SearchInput.Query.Remove("moviehash_match");
                }
            }
        }
    }
}