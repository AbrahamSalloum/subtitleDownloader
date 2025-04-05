using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using subsl.Models;

namespace subsl.Services
{
    public class OpenSubtitlesAPI
    {
        private static HttpClient _HttpClient = new HttpClient();
        private static string? _token;
        private static string _BaseURL = "api.opensubtitles.com";

        public OpenSubtitlesAPI()
        {
            _token =  subsl.Properties.Settings.Default.token;
        }

        public async Task<LoginOutput?> Login()
        {
            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Post, "https://api.opensubtitles.com/api/v1/login");

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

            subsl.Properties.Settings.Default.token = _token;
            
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
            if(!response.IsSuccessStatusCode)
            {
                
                return null;
            }
            return await response.Content.ReadFromJsonAsync<SearchResults?>();
        }

        public async Task<DownloadLinkInfo?> RequestDownloadInfo(int? SubId)
        {
            if(SubId == null)
            {
                return null;
            }
            
            string BodyText = $"{{\n  \"file_id\": {SubId}\n}}";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"https://{_BaseURL}/api/v1/download");
            request.Headers.Add("Api-key", $"{LoginInput.apikey}");
            request.Headers.Add("User-Agent", "a123");
            request.Headers.Add("Accept", "application/json");
            if(_token != null)
            {
                request.Headers.Add("Authorization", $"Bearer {_token}");
            }
            request.Content = new StringContent(BodyText, Encoding.UTF8, "application/json");
            var response = await _HttpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
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
