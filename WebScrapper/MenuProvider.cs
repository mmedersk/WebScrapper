using System;
using System.Collections.Generic;
using System.Configuration;

namespace WebScrapper
{
    public class MenuProvider
    {
        public static void PrintMenu()
        {
            Console.WriteLine("\n=============================");
            Console.WriteLine("1. Extract \n2. Transform \n3. Log \n4. ETL \n5. Eksport do CSV \n6. Wyjscie");
            Console.WriteLine("=============================\n");
        }
    }
}
