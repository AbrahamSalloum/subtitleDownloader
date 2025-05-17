using subsl.Models;
using System.Configuration;
using System.Data;
using System.Windows;

namespace subsl
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {

            MpvInput.Filepath = e.Args[0];
        }
    }

}
