using System;
using System.Net;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Net.Http.OData;
using Xunit;

namespace Net.Http.WebApi.OData.Tests
{
    public class ODataExceptionFilterAttributeTests
    {
        [Fact]
        public void OnException_DoesNotSet_Response_If_Exception_IsNot_ODataException()
        {
            var actionExecutedContext = new HttpActionExecutedContext
            {
                ActionContext = new HttpActionContext
                {
                    ControllerContext = new HttpControllerContext
                    {
                        Request = TestHelper.CreateODataHttpRequest("/OData/Products"),
                    }
                },
                Exception = new ArgumentNullException("Name"),
            };

            var exceptionFilter = new ODataExceptionFilterAttribute();
            exceptionFilter.OnException(actionExecutedContext);

            Assert.Null(actionExecutedContext.Response);
        }

        [Fact]
        public void OnException_DoesNotSet_Response_If_Exception_IsNull()
        {
            var actionExecutedContext = new HttpActionExecutedContext
            {
                ActionContext = new HttpActionContext
                {
                    ControllerContext = new HttpControllerContext
                    {
                        Request = TestHelper.CreateODataHttpRequest("/OData/Products"),
                    }
                },
                Exception = null,
            };

            var exceptionFilter = new ODataExceptionFilterAttribute();
            exceptionFilter.OnException(actionExecutedContext);

            Assert.Null(actionExecutedContext.Response);
        }

        [Fact]
        public void OnException_Sets_Response_If_Exception_Is_ODataException()
        {
            var actionExecutedContext = new HttpActionExecutedContext
            {
                ActionContext = new HttpActionContext
                {
                    ControllerContext = new HttpControllerContext
                    {
                        Request = TestHelper.CreateODataHttpRequest("/OData/Products?$select=Foo"),
                    }
                },
                Exception = new ODataException("The type 'NorthwindModel.Product' does not contain a property named 'Foo'", HttpStatusCode.BadRequest),
            };

            var exceptionFilter = new ODataExceptionFilterAttribute();
            exceptionFilter.OnException(actionExecutedContext);

            Assert.NotNull(actionExecutedContext.Response);
        }
    }
}
