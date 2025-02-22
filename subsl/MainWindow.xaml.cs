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

        public MainWindow()
        {
            InitializeComponent();
            
            this.DataContext = this;
            Subtitles = new ObservableCollection<ItemList>();
            FeatureType = new ObservableCollection<String>();
            Langauges = new ObservableCollection<String>();
            CurrentSelected = new ItemList(); 
        }



        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string InputQueryText = searchBarInput.Text;

            if (InputQueryText != "")
            {
                SearchInput search = new SearchInput
                {
                    query = InputQueryText,
                    languages = "en"
                };

                SearchSubtitle(search);
            }


        }

        private async void SearchSubtitle(SearchInput search)
        {
            OpenSubtitlesAPI subs = new OpenSubtitlesAPI();

            if (subs != null)
            {

                    SearchResults SubtitleSearchResults = await subs.Search(search);

                    if (SubtitleSearchResults?.data != null)
                    {
                        StatusText.Visibility = Visibility.Hidden;
                        StatusTextGrid.Visibility = Visibility.Collapsed;
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
                        StatusText.Visibility = Visibility.Visible;
                        StatusTextGrid.Visibility = Visibility.Visible;
                    }
            }
        }

        private void ListViewItem_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var listView = sender as ListView;
            CurrentSelected = listView?.SelectedItem as ItemList;
            Debug.WriteLine(CurrentSelected?.attributes?.url);
            MessageBox.Show($"subtitle id: {CurrentSelected?.attributes?.subtitle_id}");
            
        }
        private void ListViewItem_MouseClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var listView = sender as ListView;
            CurrentSelected = listView?.SelectedItem as ItemList;
            if (CurrentSelected?.attributes?.related_links?[0]?.img_url != null)
            {
                PosterStatus.Text = "Loading Poster";
                string imgurl = CurrentSelected.attributes.related_links[0].img_url;
                img.Source = new BitmapImage(new Uri(imgurl));
            } else
            {
                PosterStatus.Text = "No Poster Found";
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

        private void DownLoadSub_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "sub";
            saveFileDialog.Filter = "Subtitles (*.sub)|*.sub|All files (*.*)|*.*";

            if (CurrentSelected?.attributes?.subtitle_id != null)
            {
                saveFileDialog.FileName = $"{CurrentSelected?.attributes?.subtitle_id}.sub";
            }
                
            if (saveFileDialog.ShowDialog() == true)
            {
                Debug.WriteLine(saveFileDialog.FileName);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            OptionsWindow options = new OptionsWindow();
            options.Show();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filename = openFileDialog.FileName;
                byte[] moviehash = MovieHash.ComputeMovieHash($"{filename}");
                string hash = MovieHash.ToHexadecimal(moviehash);
                

                if (hash != "")
                {
                    SearchInput search = new SearchInput
                    {
                        moviehash = hash,
                    };

                    SearchSubtitle(search);
                }
            }

        }
    }

    }