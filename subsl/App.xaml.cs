
using System.Windows;
using System.Text.Json;
using System.Text.Json.Nodes;
using subsl.Models;
using System.Text.RegularExpressions;


namespace subsl
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {

            if(e.Args.Length > 0)
            {
                var s = e.Args[0].Replace("\\", "\\\\");
                var jsonObj = JsonNode.Parse(s).AsObject();
                MpvInput.Filepath = jsonObj["filepath"].ToString();
            }

        }
    }

}
