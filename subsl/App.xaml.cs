
using System.Windows;
using System.Text.Json;
using System.Text.Json.Nodes;
using subsl.Models;
using System.Text.RegularExpressions;


namespace subsl
{

    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {

            if(e.Args.Length > 0)
            {
                string s = e.Args[0].Replace("\\", "\\\\");
                if(s is not null)
                {
                    JsonNode? jsonObj = JsonNode.Parse(s)?.AsObject();
                    if(jsonObj is not null)
                    {
                        if (jsonObj["filepath"] is not null)
                        {
                            MpvInput.Filepath = jsonObj["filepath"]?.ToString();
                        }
                       
                    }
                }

            }

        }
    }

}
