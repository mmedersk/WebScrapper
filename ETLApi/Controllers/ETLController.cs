using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebScrapper;

namespace ETLApi.Controllers
{
    
    public class ETLController : ApiController
    {
        [Route("api/ETL/Extract")]
        [HttpPost]
        public IHttpActionResult Get(JsonBodyModel model)
        {
            var scrapper = new ETLHandler.WebScrapper(model.url_adress);

            var result = scrapper.GetRawHtmls(needSave: true);

            return Ok($"Scrapped {result.Count} pages");
        }

        [Route("api/ETL/Transform")]
        [HttpGet]
        public IHttpActionResult Transform()
        {
            var transformer = new ETLHandler.TransformationHandler();
            var results = transformer.GetListOfProducts(needSave: true, rawHtmlList: null);

            return Ok($"transformed {results.Count} results");
        }

        [Route("api/ETL/Load")]
        [HttpGet]
        public IHttpActionResult Load()
        {
            var dbHandler = new DatabaseHandler();
            dbHandler.Load(null);

            return Ok();
        }


        [Route("api/ETL/fulletl")]
        [HttpGet]
        public IHttpActionResult FullETL(JsonBodyModel model)
        {
            var scrapper = new ETLHandler.WebScrapper(model.url_adress);
            var htmlFiles = scrapper.GetRawHtmls(needSave: false);

            var transformer = new ETLHandler.TransformationHandler();
            var transformationResults = transformer.GetListOfProducts(needSave: false, rawHtmlList: htmlFiles);

            var dbHandler = new DatabaseHandler();
            dbHandler.Load(transformationResults);

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
