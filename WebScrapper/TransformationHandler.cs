using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CommonItems;
using HtmlAgilityPack;

namespace ETLHandler
{
    public class TransformationHandler
    {
        public List<ListingItemModel> GetListOfProducts()
        {
            var filesPaths = GetPathsToHTMLFiles();
            var results = new List<ListingItemModel>();
            string rawHtml = String.Empty;
            foreach (var file in filesPaths)
            {

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

                var detailsSection = htmlDoc.DocumentNode.Descendants("div")
                    .Where(x => x.GetAttributeValue("class", "")
                        .Equals("css-1kgyoyz-Xt"))
                    .Single()
                    .FirstChild;

                results.Add(GetOfferDetails(detailsSection, titleHeader));
            }

            return results;
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
            //TODO: get details from title node (currently only details is processed)
            offer.Area = details.ChildNodes.SingleOrDefault(li => li.InnerHtml.Contains("Powierzchnia"))?.FirstChild.NextSibling.InnerText;
            offer.Bond = details.ChildNodes.SingleOrDefault(li => li.InnerHtml.Contains("Kaucja"))?.FirstChild.NextSibling.InnerText;
            offer.BuildingType = details.ChildNodes.SingleOrDefault(li => li.InnerHtml.Contains("Rodzaj zabudowy"))?.FirstChild.NextSibling.InnerText;
            offer.BuiltIn = details.ChildNodes.SingleOrDefault(li => li.InnerHtml.Contains("Rok budowy"))?.FirstChild.NextSibling.InnerText;
            offer.Floor = details.ChildNodes.SingleOrDefault(li => li.InnerHtml.Contains("Piętro"))?.FirstChild.NextSibling.InnerText;
            offer.FloorsInBuilding = details.ChildNodes.SingleOrDefault(li => li.InnerHtml.Contains("Liczba pięter"))?.FirstChild.NextSibling.InnerText;
            offer.HeatingType = details.ChildNodes.SingleOrDefault(li => li.InnerHtml.Contains("Ogrzewanie"))?.FirstChild.NextSibling.InnerText;
            offer.Materials = details.ChildNodes.SingleOrDefault(li => li.InnerHtml.Contains("Materiał budynku"))?.FirstChild.NextSibling.InnerText;
            offer.Rooms = details.ChildNodes.SingleOrDefault(li => li.InnerHtml.Contains("Liczba pokoi"))?.FirstChild.NextSibling.InnerText;
            offer.Windows = details.ChildNodes.SingleOrDefault(li => li.InnerHtml.Contains("Okna"))?.FirstChild.NextSibling.InnerText;
            return offer;
        }

        private List<string> GetPathsToHTMLFiles()
        {
            return Directory.EnumerateFiles(Path.Combine(Path.GetTempPath(), "ETLArtifacts")).ToList();
        }
    }
}
