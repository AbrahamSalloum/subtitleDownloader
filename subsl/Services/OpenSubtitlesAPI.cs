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

        private static string? _ApiKey;
        private static string? _UserName;
        private static string? _Password;


        public OpenSubtitlesAPI()
        {
            LoginInput? cred;
            using (StreamReader r = new StreamReader("./cred.json"))
            {
                string file = r.ReadToEnd();
                cred = JsonSerializer.Deserialize<LoginInput>(file);
            }

            if(cred == null)
            {
                throw new Exception("Invalid Credentials");
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
            if (logininfo == null)
            {
                return null;
            }

            _token = logininfo.token;
            _BaseURL = logininfo.base_url;
            Debug.WriteLine($"Token: {_token}");
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
            msg.Headers.Add("Api-key", $"{_ApiKey}");

            var response = await _HttpClient.SendAsync(msg);
            return await response.Content.ReadFromJsonAsync<SearchResults?>();
        }


        public async Task<DownloadLinkInfo?> RequestDownloadInfo(string? SubId)
        {
            if(SubId == null)
            {
                return null;
            }
            int SubIdInt = Int32.Parse(SubId);
            string BodyText = $"{{\n  \"file_id\": {SubIdInt}\n}}";

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"https://{_BaseURL}/api/v1/download"),
                Headers =
                    {
                        { "User-Agent", "a123" },
                        {"Api-Key", $"{_ApiKey}" },
                        { "Accept", "application/json" },
                        { "Authorization", $"Bearer {_token}"}
                        
                    },
                Content = new StringContent(BodyText, Encoding.UTF8, "application/json")
            };

            var response = await _HttpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Request failed with status code {response.StatusCode}. Content: {content}");
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
