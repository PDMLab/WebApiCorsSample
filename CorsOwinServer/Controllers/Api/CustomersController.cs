using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CorsOwinServer.Controllers.Api
{
    public class CustomersController : ApiController
    {
        [Authorize]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Customer> Get()
        {
            return new List<Customer>()
                {
                    new Customer()
                        {
                            Id = "1",
                            Name = "ALFKI"
                        },
                    new Customer()
                        {
                            Id = "2",
                            Name = "ERNSH"
                        }
                };
        } 
    }
}