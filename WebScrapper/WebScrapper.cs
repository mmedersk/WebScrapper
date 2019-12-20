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
        private TransformationHandler _transformer;

        public WebScrapper(string url)
        {
            this._url = url;
            _httpClient = new HttpClient();
            _transformer = new TransformationHandler();
        }

        public string GetRawHtmls()
        {
            var html = _httpClient.GetStringAsync(_url);
            Task.WaitAny(html);
            var urls =_transformer.GetAdsUrls(html.Result);
            var i = 1;
            foreach (var url in urls)
            {
                var adHtml = _httpClient.GetStringAsync(url);
                SaveRawHtmlToText(adHtml.Result, i);
                i++;
            }
            return html.Result;
        }

        private void SaveRawHtmlToText(string result, int documentNumber)
        {
            var pathToTempDirectory = Path.Combine(Path.GetTempPath(), "ETLArtifacts");

            if (!Directory.Exists(pathToTempDirectory))
            {
                Directory.CreateDirectory(pathToTempDirectory);
            }

            var pathToRawHtml = Path.Combine(pathToTempDirectory, $"rawHtml{documentNumber}.txt");
            using (StreamWriter sw = File.CreateText(pathToRawHtml))
            {
                sw.Write(result);
            }
        }
    }
}