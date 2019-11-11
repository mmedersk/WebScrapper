using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CommonItems;
using HtmlAgilityPack;

namespace WebScrapper
{
    public class WebScrapper
    {
        // otodom.pl -> Warszawa -> +5km radius -> 72 Ads per page
        private const string url =
            "https://www.otodom.pl/wynajem/mieszkanie/warszawa/?search%5Bregion_id%5D=7&search%5Bsubregion_id%5D=197&search%5Bcity_id%5D=26&search%5Bdist%5D=5&nrAdsPerPage=72";
        private HttpClient _httpClient;

        public WebScrapper()
        {
            _httpClient = new HttpClient();
        }

        public List<ListingItemModel> GetListings()
        {
            var html = _httpClient.GetStringAsync(url);
            Task.WaitAny(html);
            return GetListOfProducts(html.Result);
        }

        private List<ListingItemModel> GetListOfProducts(string rawHtml)
        {
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

            return product.Descendants(attribute)
                .Where(x => x.GetAttributeValue("class", "")
                    .Equals(itemDetail))
                .First()
                .InnerText;
        }
    }

    public enum ListingDetailName
    {
        Title,
        Rooms,
        Area,
        Price
    }
}