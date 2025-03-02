using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using subsl.Models;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Diagnostics;

namespace subsl.Services
{
    public class OpenSubtitlesAPI
    {
        private static HttpClient _HttpClient = new HttpClient();
        private static string? _token;
        private static string _BaseURL = "api.opensubtitles.com";

        //private static string? _ApiKey;
        //private static string? _UserName;
        //private static string? _Password;

        public OpenSubtitlesAPI()
        {

            //_ApiKey = LoginInput.apikey;
            //_UserName = LoginInput.username;
            //_Password = LoginInput.password;
        }

        public async Task<LoginOutput?> Login()
        {
            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Post, "https://api.opensubtitles.com/api/v1/login");
            if(Properties.Settings.Default.token != "" && Properties.Settings.Default.token != null)
            {
                return null;
            }

            if(LoginInput.username == null || LoginInput.password == null)
            {
                return null; 
            }



            msg.Headers.Add("Api-key", $"{LoginInput.apikey}");
            msg.Headers.Add("User-Agent", "a123");
            msg.Content = new StringContent($"{{\r\n  \"username\": \"{LoginInput.username}\",\r\n  \"password\": \"{LoginInput.password}\"\r\n}}", Encoding.UTF8, "application/json");
            var response = await _HttpClient.SendAsync(msg);
            response.EnsureSuccessStatusCode();
            var logininfo = await response.Content.ReadFromJsonAsync<LoginOutput>();
            if (logininfo == null)
            {
                return null;
            }

            _token = logininfo.token;
            Properties.Settings.Default.token = _token;
            Properties.Settings.Default.Save();
            _BaseURL = logininfo.base_url;
            
            return logininfo;
        }

        public async Task<SearchResults?> Search(Dictionary<string, object>? SearchBoxInput)
        {

            if(SearchBoxInput == null)
            {
                return null;
            }
            string qqueryparam = "";
            foreach (var item in SearchBoxInput)
            {
                qqueryparam += $"&{item.Key}={item.Value}";
            }


            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Get, $"https://{_BaseURL}/api/v1/subtitles?{qqueryparam}");

            msg.Headers.Add("User-Agent", $"a123");
            msg.Headers.Add("Api-key", $"{LoginInput.apikey}");

            var response = await _HttpClient.SendAsync(msg);
            return await response.Content.ReadFromJsonAsync<SearchResults?>();
        }

        public async Task<DownloadLinkInfo?> RequestDownloadInfo(int? SubId)
        {
            if(SubId == null)
            {
                return null;
            }
            
            string BodyText = $"{{\n  \"file_id\": {SubId}\n}}";

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"https://{_BaseURL}/api/v1/download"),
                Headers =
                    {
                        { "User-Agent", "a123" },
                        {"Api-Key", $"{LoginInput.apikey}" },
                        { "Accept", "application/json" },
                        { "Authorization", $"Bearer {_token}"}
                        
                    },
                Content = new StringContent(BodyText, Encoding.UTF8, "application/json")
            };

            var response = await _HttpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Properties.Settings.Default.token = null;
                Properties.Settings.Default.Save();

                return new DownloadLinkInfo() { message = "error"};

            }

            DownloadLinkInfo? DownloadInfo = await response.Content.ReadFromJsonAsync<DownloadLinkInfo>();
            return DownloadInfo;
        }

        public async Task DownloadSubtitle(string DownloadUrl, string FileName, string FilePath)
        {
            using (var s = await _HttpClient.GetStreamAsync(DownloadUrl))
            {
                using (var fs = new FileStream($"{FilePath}{FileName}", FileMode.CreateNew))
                {
                    await s.CopyToAsync(fs);
                }
            }
        }

        public async Task DownloadSubtitle(string DownloadUrl, string FullPath)
        {
            using (var s = await _HttpClient.GetStreamAsync(DownloadUrl))
            {
                using (var fs = new FileStream($"{FullPath}", FileMode.CreateNew))
                {
                    await s.CopyToAsync(fs);
                }
            }
        }

    }
}
