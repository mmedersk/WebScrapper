using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ETLHandler
{
    public class WebScrapper
    {
        // otodom.pl
        private readonly string _url;
        private HttpClient _httpClient;

        public WebScrapper(string url)
        {
            this._url = url;
            _httpClient = new HttpClient();
        }

        public string GetRawHtml()
        {
            var html = _httpClient.GetStringAsync(_url);
            Task.WaitAny(html);
            SaveRawHtmlToText(html.Result);
            return html.Result;
        }

        private void SaveRawHtmlToText(string result)
        {
            var pathToTempDirectory = Path.Combine(Path.GetTempPath(), "ETLArtifacts");

            if (!Directory.Exists(pathToTempDirectory))
            {
                Directory.CreateDirectory(pathToTempDirectory);
            }

            var pathToRawHtml = Path.Combine(pathToTempDirectory, "rawHtml.txt");
            using (StreamWriter sw = File.CreateText(pathToRawHtml))
            {
                sw.Write(result);
            }
        }
    }
}