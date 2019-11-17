using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ETLApi.Controllers
{
    public class ETLController : ApiController
    {
        [Route("api/ETL/Extract/{city}/{numberOfAds:int}")]
        [HttpGet]
        public string Get(string city, int numberOfAds)
        {
            var scrapper = new ETLHandler.WebScrapper(city, numberOfAds);
            return scrapper.GetRawHtml();
        }
        [Route("api/ETL/Transform")]
        [HttpGet]
        public string Transform()
        {
            var transformer = new ETLHandler.TransformationHandler();
            var results = transformer.GetListOfProducts();

            return $"transformed {results.Count} results";
        }

        // POST: api/ETL
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ETL/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ETL/5
        public void Delete(int id)
        {
        }
    }
}
