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
        private List<ListingItemModel> _offers;

        public DatabaseHandler(List<ListingItemModel> offers)
        {
            if (offers == null)
            {
                _offers = GetOffersFromJson();
            }
            else
            {
                this._offers = offers;
            }
        }

        public DatabaseHandler()
        { }

        //TODO: Wrzucic oferty do DB i zwrocic tylko te oferty ktore zostaly wrzucone do DB (bez duplikatow)
        public List<ListingItemModel> Load()
        {
            return _offers;
        }

        //TODO: czyszczenie DB
        public void CleanDb()
        {

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
