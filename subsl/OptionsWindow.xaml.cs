using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    
    public partial class OptionsWindow : Window
    {

        public List<string> OptionsWithOnly { get; set; }
        public List<string> OptionsWithOutOnly { get; set; }
        public OptionsWindow()
        {
            InitializeComponent();
           
             OptionsWithOnly = ["include", "exclude", "only"];
             OptionsWithOutOnly = ["include", "exclude"];

            DataContext = this;
        }
    }
}
