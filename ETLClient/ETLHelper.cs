using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapper
{
    public class ETLHelper
    {
        public void Extract()
        {
            Console.WriteLine("performing extraction");
            RuntimeInfo.AppState = AppState.Extract;
        }

        public void Transform()
        {
            Console.WriteLine("performing transformation");
            RuntimeInfo.AppState = AppState.Transform;
        }

        public void Log()
        {
            Console.WriteLine("performing logging");
            RuntimeInfo.AppState = AppState.Log;
        }

        public void PerformEtl()
        {
            Console.WriteLine("performing ETL");
            RuntimeInfo.AppState = AppState.ETL;
        }
    }

    public enum AppState
    {
        Done,
        Extract,
        Transform,
        Log,
        ETL,
        ExportToCSV,
        Quit
    }
}
