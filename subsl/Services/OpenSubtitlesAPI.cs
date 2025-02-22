using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;
using subsl.Models;
using System.Diagnostics;
using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace subsl.Services
{
    public class OpenSubtitlesAPI
    {
        private static HttpClient _HttpClient = new HttpClient();
        private static string _token;
        private static string _BaseURL = "api.opensubtitles.com";

        private static string _ApiKey;
        private static string _UserName;
        private static string _Password;

        
        public OpenSubtitlesAPI() 
        {
            LoginInput? cred;
            using (StreamReader r = new StreamReader("./cred.json"))
            {
                string file = r.ReadToEnd();
                cred = JsonSerializer.Deserialize<LoginInput>(file);
            }

            _ApiKey = cred.apikey;
            _UserName = cred.username;
            _Password = cred.password;
        }

        public async Task<LoginOutput?> Login()
        {
            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Post, "https://api.opensubtitles.com/api/v1/login");

            msg.Headers.Add("Api-key", $"{_ApiKey}");
            msg.Headers.Add("User-Agent", "a123");

            msg.Content = new StringContent($"{{\r\n  \"username\": \"{_UserName}\",\r\n  \"password\": \"{_Password}\"\r\n}}", Encoding.UTF8, "application/json");
            var response = await _HttpClient.SendAsync(msg);
            response.EnsureSuccessStatusCode();
            var logininfo = await response.Content.ReadFromJsonAsync<LoginOutput>();
            if(logininfo == null)
            {
                return null;
            }

            _token = logininfo.token;
            _BaseURL = logininfo.base_url;
            return logininfo;
        }

        public async Task<SearchResults?> Search(SearchInput SearchBoxInput)
        {
            string qqueryparam = ""; 
            foreach(var item in SearchBoxInput.GetType().GetProperties())
            {
                if (item.GetValue(SearchBoxInput) != null)
                {
                    qqueryparam += $"&{item.Name}={item.GetValue(SearchBoxInput)}";
                } 
            }


            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Get, $"https://{_BaseURL}/api/v1/subtitles?{qqueryparam}");

            msg.Headers.Add("User-Agent", $"as123");
            msg.Headers.Add("Api-key", $"{_ApiKey}");

            var response = await _HttpClient.SendAsync(msg);
            return await response.Content.ReadFromJsonAsync<SearchResults?>();
        }

        
        public async Task<DownloadLinkInfo?> RequestDownloadInfo(string SubId)
        {
            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Get, $"https://{_BaseURL}/api/v1/subtitles/{SubId}/download");
            
            msg.Headers.Add("User-Agent", $"a123");
            msg.Headers.Add("Accept", $"application/json");
            msg.Headers.Add("Authorization", $"Bearer {_token}");
            
            msg.Content = new StringContent($"{{\r\n  \"file_id\": {SubId}\r\n}}", Encoding.UTF8, "application/json");
            var response = await _HttpClient.SendAsync(msg);

            response.EnsureSuccessStatusCode();
            DownloadLinkInfo DownloadInfo = await response.Content.ReadFromJsonAsync<DownloadLinkInfo>();
            return DownloadInfo;
        }

        public static async Task DownloadSubtitle(string DownloadUrl, string path, string FileName, string FilePath)
        {
            using (var s = await _HttpClient.GetStreamAsync(DownloadUrl))
            {
                using (var fs = new FileStream($"{FilePath}{FileName}", FileMode.CreateNew))
                {
                    await s.CopyToAsync(fs);
                }
            }
        }

    }
}
