using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonItems;
using Newtonsoft.Json;
using System.Diagnostics;

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
            Process.Start("C:\\WebScrapper/DataBase/FunkcjeBazyDanych/Load_i_Merge.exe");
            return _offers;
        }

        public void CleanDb()
        {
            Process.Start("C:\\WebScrapper/DataBase/FunkcjeBazyDanych/Delete_Table.exe");
        }

        //TODO: zaczytac wszystkie recordy z DB i zwrocic jako liste
        public List<ListingItemModel> GetAllRecords()
        {
            return null;
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
