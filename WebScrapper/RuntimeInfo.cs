using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapper
{
    static class RuntimeInfo
    {
        public static AppState AppState { get; set; } = (AppState) int.Parse(ConfigurationManager.AppSettings["appState"]);
    }
}
