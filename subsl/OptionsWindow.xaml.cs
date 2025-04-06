using subsl.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace subsl
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    
    public partial class OptionsWindow : Window, INotifyPropertyChanged
    {

        public List<string> OptionsWithOnly { get; set; }
        public List<string> OptionsWithOutOnly { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public OptionsWindow()
        {
            InitializeComponent();
           
            OptionsWithOnly = ["", "include", "exclude", "only"];
            OptionsWithOutOnly = ["", "include", "exclude"];

            DataContext = this;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            SearchInput.AddQuery("ai_translated", ai_translatedV);
            SearchInput.AddQuery("machine_translated", machine_translatedV);
            SearchInput.AddQuery("foreign_parts_only", foreign_parts_onlyV);
            SearchInput.AddQuery("hearing_impaired", hearing_impairedV);
            SearchInput.AddQuery("imdb_id", imdb_idV);
            SearchInput.AddQuery("tmdb_id", tmdb_idV);
            SearchInput.AddQuery("episode_number", episode_numberV);
            SearchInput.AddQuery("season_number", season_numberV);
            this.Close();
        }

        private string? _episode_numberV = (string?)SearchInput.GetQueryValue("episode_number");
        public string? episode_numberV
        {
            get { return _episode_numberV; }
            set
            {
                if (_episode_numberV != value)
                {
                    _episode_numberV = value;
                    NotifyPropertyChanged(nameof(_episode_numberV));
                }
            }
        }

        private string? _season_numberV = (string?)SearchInput.GetQueryValue("season_number");
        public string? season_numberV
        {
            get { return _season_numberV; }
            set
            {
                if (_season_numberV != value)
                {
                    _season_numberV = value;
                    NotifyPropertyChanged(nameof(_season_numberV));
                }
            }
        }

        private string? _imdb_idV = (string?)SearchInput.GetQueryValue("imdb_id");
        public string? imdb_idV
        {
            get { return _imdb_idV; }
            set
            {
                if (_imdb_idV != value)
                {
                    _imdb_idV = value;
                    NotifyPropertyChanged(nameof(_imdb_idV));
                }
            }
        }

        private string? _tmdb_idV = (string?)SearchInput.GetQueryValue("tmdb_id");
        public string? tmdb_idV
        {
            get { return _tmdb_idV; }
            set
            {
                if (_tmdb_idV != value)
                {
                    _tmdb_idV = value;
                    NotifyPropertyChanged(nameof(_tmdb_idV));
                }
            }
        }

        private string? _ai_translatedV = (string?)SearchInput.GetQueryValue("ai_translated");
        public string? ai_translatedV
        {
            get { return _ai_translatedV; }
            set
            {
                if (_ai_translatedV != value)
                {
                    _ai_translatedV = value;
                    NotifyPropertyChanged(nameof(_ai_translatedV));
                }
            }
        }

        private string? _machine_translatedV = (string?)SearchInput.GetQueryValue("machine_translated");
        public string? machine_translatedV
        {
            get { return _machine_translatedV; }
            set
            {
                if (_machine_translatedV != value)
                {
                    _machine_translatedV = value;
                    NotifyPropertyChanged(nameof(_machine_translatedV));
                }
            }
        }

        private string? _foreign_parts_onlyV = (string?)SearchInput.GetQueryValue("foreign_parts_only");
        public string? foreign_parts_onlyV
        {
            get { return _foreign_parts_onlyV; }
            set
            {
                if (_foreign_parts_onlyV != value)
                {
                    _foreign_parts_onlyV = value;
                    NotifyPropertyChanged(nameof(_foreign_parts_onlyV));
                }
            }
        }

        private string? _hearing_impairedV = (string?)SearchInput.GetQueryValue("hearing_impaired");
        public string? hearing_impairedV
        {
            get { return _hearing_impairedV; }
            set
            {
                if (_hearing_impairedV != value)
                {
                    _hearing_impairedV = value;
                    NotifyPropertyChanged(nameof(_hearing_impairedV));
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
