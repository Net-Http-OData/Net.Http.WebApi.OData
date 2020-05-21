using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Net.Http.OData;
using Net.Http.OData.Query;
using Sample.Model;

namespace Net.Http.WebApi.OData.Tests.Integration
{
    /// <summary>
    /// An implementation for integration tests against an entity set.
    /// </summary>
    [RoutePrefix("odata/Employees")]
    public sealed class EmployeeController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get(ODataQueryOptions queryOptions)
        {
            string odataContext = Request.ODataContext(queryOptions.EntitySet, queryOptions.Select);
            IEnumerable<Employee> value = Enumerable.Empty<Employee>();

            var odataResponseContent = new ODataResponseContent
            {
                Context = odataContext,
                Value = value,
            };

            return Ok(odataResponseContent);
        }
    }
}
