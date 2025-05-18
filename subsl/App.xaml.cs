
using System.Windows;
using System.IO;
using System.Text.Json.Nodes;
using subsl.Models;



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
                            if (jsonObj["filepath"] is not null)
                            {
                                string filepath = jsonObj["filepath"].ToString();
                                if (System.IO.File.Exists(filepath))
                                {
                                    MpvInput.Filepath = filepath;
                                } else 
                                {
                                    MessageBox.Show("File not found: " + filepath, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    Environment.Exit(0);
                                }

                                if (jsonObj["filename"] is not null)
                                {
                                    MpvInput.Filename = jsonObj["filename"].ToString();
                                }
                            }
                                
                        }
                       
                    }
                }

            }

        }
    }

}
