using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Eventing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapper
{
    class Program
    {
        private ETLHelper etlHelper;

        static void Main(string[] args)
        {
            var program = new Program();
            program.etlHelper = new ETLHelper();
            string input;
            while (true)
            {
                MenuProvider.PrintMenu();
                do
                {
                    input = Console.ReadLine();
                } while (program.ValidateInput(input, out var step));
            }
        }

        private bool ValidateInput(string input, out int step)
        {
            if (int.TryParse(input, out step))
            {
                var chosenStep = (AppState)step;
                PerformStep(chosenStep);
                return false;
            }
            Console.WriteLine("Podano niepoprawna wartosc. Prosze sprobowac ponownie.");
            return true;
        }

        private void PerformStep(AppState step)
        {
            var appState = RuntimeInfo.AppState;

            switch (step)
            {
                case AppState.Extract:
                    etlHelper.Extract();
                    break;
                case AppState.Transform:
                    if (appState == AppState.Extract)
                        etlHelper.Transform();
                    else
                        WriteErrorMessage();
                    break;
                case AppState.Log:
                    if (appState == AppState.Transform)
                        etlHelper.Log();
                    else
                        WriteErrorMessage();
                    break;
                case AppState.ETL:
                    etlHelper.PerformEtl();
                    break;
                case AppState.ExportToCSV:
                    CSVExporter.ExportToCSV();
                    break;
                case AppState.Quit:
                    ExitApp();
                    break;
                default:
                    Console.WriteLine("Podano niepoprawna wartosc. Prosze sprobowac ponownie.");
                    break;
            }

            void WriteErrorMessage()
            {
                Console.WriteLine("Nie mozna wykonac tej operacji, upewnij sie, ze podajesz wlasciwa wartosc.");
            }
        }

        private void ExitApp()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["appState"].Value = ((int) RuntimeInfo.AppState).ToString();
            config.Save();
            Environment.Exit(0);
        }
    }
}
