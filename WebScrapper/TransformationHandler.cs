using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using CommonItems;
using HtmlAgilityPack;

namespace ETLHandler
{
    public class TransformationHandler
    {
        public List<ListingItemModel> GetListOfProducts()
        {
            string rawHtml = GetRawHtmlFromFile();
            var results = new List<ListingItemModel>();
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(rawHtml);

            //Get html part with all listings
            var products = htmlDoc.DocumentNode.Descendants("div")
                .Where(x => x.GetAttributeValue("class", "")
                    .Equals("col-md-content section-listing__row-content"))
                .ToList();

            //Get list of listings
            var productsList = products[0].Descendants("article")
                .Where(x => x.GetAttributeValue("id", "")
                    .Contains("offer"))
                .ToList();

            foreach (var product in productsList)
            {
                results.Add(new ListingItemModel()
                {
                    Title = GetProductDetail(product, ListingDetailName.Title),
                    Rooms = GetProductDetail(product, ListingDetailName.Rooms),
                    Area = GetProductDetail(product, ListingDetailName.Area),
                    Price = GetProductDetail(product, ListingDetailName.Price)
                });
            }
            return results;
        }

        private string GetRawHtmlFromFile()
        {
            var pathToRawHtml = Path.Combine(Path.GetTempPath(), "ETLArtifacts",
                "rawHtml.txt");
            using (StreamReader sr = File.OpenText(pathToRawHtml))
            {
                return sr.ReadToEnd();
            }
        }

        private string GetProductDetail(HtmlNode product, ListingDetailName detail)
        {
            string attribute = "li";
            string itemDetail = String.Empty;

            switch (detail)
            {
                case ListingDetailName.Title:
                    itemDetail = "offer-item-title";
                    attribute = "span";
                    break;
                case ListingDetailName.Rooms:
                    itemDetail = "offer-item-rooms hidden-xs";
                    break;
                case ListingDetailName.Area:
                    itemDetail = "hidden-xs offer-item-area";
                    break;
                case ListingDetailName.Price:
                    itemDetail = "offer-item-price";
                    break;
            }

            return product?.Descendants(attribute)
                .Where(x => x.GetAttributeValue("class", "")
                    .Equals(itemDetail))
                .FirstOrDefault()?
                .InnerText;
        }
    }

    enum ListingDetailName
    {
        Title,
        Rooms,
        Area,
        Price
    }
}
