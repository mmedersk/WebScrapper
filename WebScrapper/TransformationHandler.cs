using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CommonItems;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace ETLHandler
{
    public class TransformationHandler
    {
        private static readonly char[] _polishChars = { 'ą', 'ć', 'ę', 'ł', 'ń', 'ó', 'ś', 'ź', 'ż' };
        private static readonly char[] _englishChars = { 'a', 'c', 'e', 'l', 'n', 'o', 's', 'z', 'z' };

        public List<ListingItemModel> GetListOfProducts(bool needSave, List<string> rawHtmlList)
        {
            var results = new List<ListingItemModel>();

            if (needSave)
            {
                var filesPaths = GetPathsToHTMLFiles();
                
                foreach (var file in filesPaths)
                {
                    string rawHtml = String.Empty;

                    using (StreamReader sr = File.OpenText(file))
                    {
                        rawHtml = sr.ReadToEnd();
                    }
                    var htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(rawHtml);

                    var titleHeader = htmlDoc.DocumentNode.Descendants("header")
                        .Where(x => x.GetAttributeValue("class", "")
                            .Equals("css-jcl595"))
                        .Single();

                    var detailsSection = htmlDoc.DocumentNode.Descendants("section")
                        .Where(x => x.GetAttributeValue("class", "")
                            .Equals("section-overview"))
                        .Single().ChildNodes.First(x => x.Name.Equals("div")).FirstChild;

                    results.Add(GetOfferDetails(detailsSection, titleHeader));
                }
                
                SaveTransformedResults(results);
                return results;
            }
            else
            {
                foreach (var html in rawHtmlList)
                {
                    var htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(html);

                    var titleHeader = htmlDoc.DocumentNode.Descendants("header")
                        .Where(x => x.GetAttributeValue("class", "")
                            .Equals("css-jcl595"))
                        .Single();

                    var detailsSection = htmlDoc.DocumentNode.Descendants("section")
                        .Where(x => x.GetAttributeValue("class", "")
                            .Equals("section-overview"))
                        .Single().ChildNodes.First(x => x.Name.Equals("div")).FirstChild;

                    results.Add(GetOfferDetails(detailsSection, titleHeader));
                }

                SaveTransformedResults(results);
                return results;
            }
        }

        public List<string> GetAdsUrls(string rawHtml)
        {
            var results = new List<string>();
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(rawHtml);

            var products = htmlDoc.DocumentNode.Descendants("div")
                .Where(x => x.GetAttributeValue("class", "")
                    .Equals("col-md-content section-listing__row-content"))
                .ToList();

            var productsList = products[0].Descendants("article")
                .Where(x => x.GetAttributeValue("id", "")
                    .Contains("offer"))
                .ToList();

            foreach (var product in productsList)
            {
                results.Add(product.Attributes["data-url"].Value);
            }
            return results;
        }

        private ListingItemModel GetOfferDetails(HtmlNode details, HtmlNode title)
        {
            var offer = new ListingItemModel();

            try
            {
                offer.Title = RemoveDiacritics(title.Descendants("h1")
                    .Where(x => x.GetAttributeValue("class", "")
                        .Equals("css-1ld8fwi")).Single().InnerText);
                
                offer.Address = RemoveDiacritics(title.Descendants("a")
                    .Where(x => x.GetAttributeValue("class", "")
                        .Equals("css-12hd9gg")).Single().InnerText);

                offer.Price = RemoveDiacritics(title.Descendants("div")
                    .Where(x => x.GetAttributeValue("class", "")
                        .Equals("css-1vr19r7")).Single().InnerText);

                offer.Area = RemoveDiacritics(details.ChildNodes.SingleOrDefault(li => li.InnerHtml.Contains("Powierzchnia"))?.FirstChild.NextSibling.InnerText) ?? "Brak Informacji";
                offer.Bond = ToIntFromString(details.ChildNodes.SingleOrDefault(li => li.InnerHtml.Contains("Kaucja"))?.FirstChild.NextSibling.InnerText);
                offer.BuildingType = RemoveDiacritics(details.ChildNodes.SingleOrDefault(li => li.InnerHtml.Contains("Rodzaj zabudowy"))?.FirstChild.NextSibling.InnerText) ?? "Brak Informacji";
                offer.BuiltIn = ToIntFromString(details.ChildNodes.SingleOrDefault(li => li.InnerHtml.Contains("Rok budowy"))?.FirstChild.NextSibling.InnerText);
                offer.Floor = ToIntFromString(details.ChildNodes.SingleOrDefault(li => li.InnerHtml.Contains("Piętro"))?.FirstChild.NextSibling.InnerText);
                offer.FloorsInBuilding = ToIntFromString(details.ChildNodes.SingleOrDefault(li => li.InnerHtml.Contains("Liczba pięter"))?.FirstChild.NextSibling.InnerText);
                offer.HeatingType = RemoveDiacritics(details.ChildNodes.SingleOrDefault(li => li.InnerHtml.Contains("Ogrzewanie"))?.FirstChild.NextSibling.InnerText) ?? "Brak Informacji";
                offer.Materials = RemoveDiacritics(details.ChildNodes.SingleOrDefault(li => li.InnerHtml.Contains("Materiał budynku"))?.FirstChild.NextSibling.InnerText) ?? "Brak Informacji";
                offer.Rooms = ToIntFromString(details.ChildNodes.SingleOrDefault(li => li.InnerHtml.Contains("Liczba pokoi"))?.FirstChild.NextSibling.InnerText);
                offer.Windows = RemoveDiacritics(details.ChildNodes.SingleOrDefault(li => li.InnerHtml.Contains("Okna"))?.FirstChild.NextSibling.InnerText) ?? "Brak Informacji";
            }
            catch
            {
                //ignore
            }

            return offer;

        }

        private List<string> GetPathsToHTMLFiles()
        {
            return Directory.EnumerateFiles(Path.Combine(Path.GetTempPath(), "ETLArtifacts"),"*.txt").ToList();
        }

        private int ToIntFromString(string text)
        {
            if (string.IsNullOrEmpty(text))
                return 0;

            var tempArray = text.ToCharArray();
            var stringNoLeters = new StringBuilder();

            foreach (var c in tempArray)
            {
                if (Char.IsDigit(c))
                {
                    stringNoLeters.Append(c);
                }
            }

            Int32.TryParse(stringNoLeters.ToString(), out var result);
            return result;
        }

        private string RemoveDiacritics(string text)
        {
            if (string.IsNullOrEmpty(text))
                return null;

            var chars = text.ToCharArray();
            var index = 0;

            foreach (char c in chars)
            {
                if (_polishChars.Contains(c))
                {
                    chars[index] = _englishChars[Array.IndexOf(_polishChars, c)];
                }
                index++;
            }

            return new String(chars);
        }

        private void SaveTransformedResults(List<ListingItemModel> results)
        {
            var pathToTempDirectory = Path.Combine(Path.GetTempPath(), "ETLArtifacts");

            if (!Directory.Exists(pathToTempDirectory))
            {
                Directory.CreateDirectory(pathToTempDirectory);
            }

            var pathToTransformationResults = Path.Combine(pathToTempDirectory, $"transformationResults.json");
            using (StreamWriter sw = File.CreateText(pathToTransformationResults))
            {
                sw.Write(JsonConvert.SerializeObject(results, Formatting.Indented));
            }
        }
    }
}
