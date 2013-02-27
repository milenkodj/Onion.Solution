using Onion.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Onion.Web.Api
{
    public class DemoController : ApiController
    {
        /// <summary>
        /// GET api/demo
        /// </summary>
        /// <returns>list of defaults demos</returns>
        public IQueryable<Demo> Get()
        {
            return new[]
            {
                new Demo { Id = Guid.Empty }
            }.AsQueryable();
        }

        /// <summary>
        /// POST api/demo
        /// </summary>
        /// <param name="demo">new demo sample</param>
        /// <returns>no content</returns>
        public HttpResponseMessage Post(Demo demo)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }

    public class Demo : AggregateRoot
    {
        public string Name { get; set; }
    }
}
