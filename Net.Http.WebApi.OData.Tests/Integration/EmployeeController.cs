using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Net.Http.OData;
using Net.Http.OData.Query;
using NorthwindModel;

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
            IEnumerable<Employee> value = Enumerable.Empty<Employee>();
            string odataContext = Request.ResolveODataContext(queryOptions.EntitySet, queryOptions.Select);

            var responseContent = new ODataResponseContent(value, odataContext);

            return Ok(responseContent);
        }
    }
}
