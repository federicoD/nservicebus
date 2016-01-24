using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using NServiceBus;
using Shared;

namespace WebApi.Controllers
{
    public class DataController : ApiController
    {
        private IBus _bus;

        public DataController()
        {
            _bus = WebApiConfig.MyBus;
        }

        [Route("processdata")]
        [HttpPost]
        public IHttpActionResult ProcessData(DataToProcess data)
        {
            // process the data

            // publish event
            _bus.OutgoingHeaders[CustomHeaders.GeneratedBy] = data.Sender;

            _bus.Publish<DataProcessedEvent>(x =>
            {
                x.Result = data.Data;
            });

            return Ok();
        }
    }

    public class DataToProcess
    {
        public String Data { get; set; }
        public String Sender { get; set; }
    }
}