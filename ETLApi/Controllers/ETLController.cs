using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ETLApi.Controllers
{
    
    public class ETLController : ApiController
    {
        [Route("api/ETL/Extract")]
        [HttpPost]
        public IHttpActionResult Get(JsonBodyModel model)
        {
            var scrapper = new ETLHandler.WebScrapper(model.url_adress);

            var result = new ResultModel()
            {
                HtmlResult = scrapper.GetRawHtmls()
            };

            return Ok(result);
        }
        [Route("api/ETL/Transform")]
        [HttpGet]
        public IHttpActionResult Transform()
        {
            var transformer = new ETLHandler.TransformationHandler();
            var results = transformer.GetListOfProducts();

            return Ok($"transformed {results.Count} results");
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
