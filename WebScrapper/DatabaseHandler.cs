using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonItems;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Data.SqlClient;

namespace WebScrapper
{
    public class DatabaseHandler
    {

        //TODO: Wrzucic oferty do DB i zwrocic tylko te oferty ktore zostaly wrzucone do DB (bez duplikatow)
        public List<ListingItemModel> Load()
        {
            
            Process.Start("C:\\WebScrapper/DataBase/FunkcjeBazyDanych/Load_i_Merge.exe");
            System.Threading.Thread.Sleep(5000);

            string connetionString;
            SqlConnection cnn;
            
            connetionString = @"Server=LAPTOP-8Q8KV00E;Database=SCRAPERBASE;Integrated Security=True;";
            cnn = new SqlConnection(connetionString);
            cnn.Open();

            var results = new List<ListingItemModel>();
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM DANE", cnn);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                ListingItemModel f = new ListingItemModel();
                f.Title = (string)reader["Title"];
                f.Rooms = (int)reader["Rooms"];
                f.Area = (string)reader["Area"];
                f.Price = (string)reader["Price"];
                f.Bond = (int)reader["Bond"];
                f.BuildingType = (string)reader["BuildingType"];
                f.FloorsInBuilding = (int)reader["FloorsInBuilding"];
                f.Windows = (string)reader["Windows"];
                f.BuiltIn = (int)reader["BuiltIn"];
                f.HeatingType = (string)reader["HeatingType"];
                f.Materials = (string)reader["Materials"];
                f.Floor = (int)reader["Floor"];
                f.Address = (string)reader["Address"];

                results.Add(f);
            }

            cnn.Close();
            //var result = GetOffersFromJson();
            return results;
        }

        public void CleanDb()
        {
            Process.Start("C:\\WebScrapper/DataBase/FunkcjeBazyDanych/Delete_Table.exe");
        }

        //TODO: zaczytac wszystkie recordy z DB i zwrocic jako liste
        private List<ListingItemModel> GetOffersFromJson()
        {
            var offers = new List<ListingItemModel>();
            var pathToOffers = "C:\\SCRAPPER\\dane.json";

            using (StreamReader sr = File.OpenText(pathToOffers))
            {
                var json = sr.ReadToEnd();
                offers = JsonConvert.DeserializeObject<List<ListingItemModel>>(json);
                return offers;
            }
        }

        
    }
}
