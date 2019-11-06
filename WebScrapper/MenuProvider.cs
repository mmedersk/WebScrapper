using System;
using System.Collections.Generic;
using System.Configuration;

namespace WebScrapper
{
    public class MenuProvider
    {
        public static void PrintMenu()
        {
            Console.WriteLine("=============================");
            Console.WriteLine("1. Extract \n2. Transform \n3. Log \n4. Eksport do CSV \n5. Wyjscie");
            Console.WriteLine("=============================");
        }
    }
}
