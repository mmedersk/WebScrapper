﻿using System;
using System.IO;
using CommonItems;
using CsvHelper;
using Newtonsoft.Json;

namespace WebScrapper
{
    public class CSVHandler
    {
        public void ExportToCSV()
        {
            var dbHandler = new DatabaseHandler();
            var offers = dbHandler.GetAllOffersFromDB();
            if (offers.Count == 0)
            {
                throw new Exception("Nothing in DataBase");
            }
            var pathToFile = GetPathToFile("ExportedData.csv");

            using (var writer = new StreamWriter(pathToFile))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(offers);
            }
        }

        public void ExportSingleToTxt(ListingItemModel offer)
        {
            string fileName = ValidateOfferTitle(offer.Title);
            var pathToFile = GetPathToFile(fileName + ".txt");

            using (var writer = new StreamWriter(pathToFile))
            {
                writer.Write(JsonConvert.SerializeObject(offer, Formatting.Indented));
            }
        }

        private string ValidateOfferTitle(string title)
        {
            var result = title.ToCharArray();
            var index = 0;

            foreach (var c in result)
            {
                if (c == ' ' || c == '.' || c == '/' || c == '\\' || c == ',')
                    result[index] = '_';
                index++;
            }

            return new string(result);
        }

        private string GetPathToFile(string fileName)
        {
            var desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            return Path.Combine(desktop, fileName);
        }
    }
}
