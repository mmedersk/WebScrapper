using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonItems;
using Newtonsoft.Json;

namespace WebScrapper
{
    public class DatabaseHandler
    {
        public void Load(List<ListingItemModel> offers)
        {
            if (offers == null)
            {
                offers = GetOffersFromJson();
            }
        }

        private List<ListingItemModel> GetOffersFromJson()
        {
            var offers = new List<ListingItemModel>();
            var pathToOffers = Path.Combine(Path.Combine(Path.GetTempPath(), "ETLArtifacts"), "transformationResults.json");

            using (StreamReader sr = File.OpenText(pathToOffers))
            {
                var json = sr.ReadToEnd();
                offers = JsonConvert.DeserializeObject<List<ListingItemModel>>(json);
                return offers;
            }
        }
    }
}
