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
           
             OptionsWithOnly = ["include", "exclude", "only"];
             OptionsWithOutOnly = ["include", "exclude"];

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
            this.Close();
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

        private string? _hearing_impairedV;
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
