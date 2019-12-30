using System;
using System.Collections.Generic;
using System.Web.Http;
using CommonItems;
using WebScrapper;

namespace ETLApi.Controllers
{
    
    public class ETLController : ApiController
    {
        [Route("api/ETL/Extract")]
        [HttpPost]
        public IHttpActionResult Get(JsonBodyModel model)
        {
            try
            {
                var scrapper = new ETLHandler.WebScrapper(model.url_adress);
                var result = scrapper.GetRawHtmls(needSave: true);

                return Ok($"Scrapped {result.Count} pages");
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/ETL/Transform")]
        [HttpGet]
        public IHttpActionResult Transform()
        {
            try
            {
                var transformer = new ETLHandler.TransformationHandler();
                var results = transformer.GetListOfProducts(needSave: true, rawHtmlList: null);

                return Ok($"transformed {results.Count} results");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/ETL/Load")]
        [HttpGet]
        public IHttpActionResult Load()
        {
            try
            {
                var dbHandler = new DatabaseHandler(null);
                var offersInDb = dbHandler.Load();

                var cleaningHandler = new CleaningHandler();
                cleaningHandler.DeleteArtifacts();

                return Ok(offersInDb);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [Route("api/ETL/fulletl")]
        [HttpGet]
        public IHttpActionResult FullETL(JsonBodyModel model)
        {
            try
            {
                var scrapper = new ETLHandler.WebScrapper(model.url_adress);
                var htmlFiles = scrapper.GetRawHtmls(needSave: false);

                var transformer = new ETLHandler.TransformationHandler();
                var transformationResults = transformer.GetListOfProducts(needSave: false, rawHtmlList: htmlFiles);

                var dbHandler = new DatabaseHandler(transformationResults);
                var offersInDb = dbHandler.Load();

                return Ok(offersInDb);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/ETL/exportToCsv")]
        [HttpGet]
        public IHttpActionResult ExportToCSV(List<ListingItemModel> offers)
        {
            try
            {
                var csvHelper = new CSVHandler();
                csvHelper.ExportToCSV(offers);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/ETL/exportSingleToTxt")]
        [HttpGet]
        public IHttpActionResult ExportSingleToTxt(ListingItemModel offer)
        {
            try
            {
                var csvHelper = new CSVHandler();
                csvHelper.ExportSingleToTxt(offer);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/ETL/cleanDb")]
        [HttpGet]
        public IHttpActionResult CleanDb(List<ListingItemModel> offers)
        {
            //TODO: zaimplementowac czyszczenie bazy danych
            var dbHandler = new DatabaseHandler();
            dbHandler.CleanDb();

            return Ok();
        }
    }

    public class JsonBodyModel
    {
        public string url_adress { get; set; }
    }

    class ResultModel
    {
        public string HtmlResult { get; set; }
    }
}
