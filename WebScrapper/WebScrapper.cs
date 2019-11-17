using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace ETLHandler
{
    public class WebScrapper
    {
        // otodom.pl
        private string url;
        private HttpClient _httpClient;

        public WebScrapper(string city, int numberOfAds)
        {
            url = $"https://www.otodom.pl/wynajem/mieszkanie/{city}/?search%5Bregion_id%5D=7&search%5Bsubregion_" +
                  $"id%5D=197&search%5Bcity_id%5D=26&search%5Bdist%5D=5&nrAdsPerPage={numberOfAds}";
            _httpClient = new HttpClient();
        }

        public string GetRawHtml()
        {
            var html = _httpClient.GetStringAsync(url);
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