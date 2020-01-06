namespace Net.Http.OData.Tests
{
    using System;
    using System.Dynamic;
    using System.Runtime.Serialization;
    using Net.Http.WebApi.OData;
    using Newtonsoft.Json;
    using Xunit;

    public class ODataResponseContentSerializationTests
    {
        [Fact]
        public void JsonSerializationWith_ClassContent_Count()
        {
            var item = new Thing();
            item.Name = "Coffee";
            item.Total = 2.55M;

            var responseContent = new ODataResponseContent(
                null,
                new[] { item },
                count: 12);

            var jsonResult = JsonConvert.SerializeObject(responseContent);

            Assert.Equal("{\"@odata.count\":12,\"value\":[{\"Name\":\"Coffee\",\"Total\":2.55}]}", jsonResult);
        }

        [Fact]
        public void JsonSerializationWith_Context_ClassContent_Count()
        {
            var item = new Thing();
            item.Name = "Coffee";
            item.Total = 2.55M;

            var responseContent = new ODataResponseContent(
                new Uri("http://services.odata.org/OData/$metadata#Products"),
                new[] { item },
                count: 12);

            var jsonResult = JsonConvert.SerializeObject(responseContent);

            Assert.Equal("{\"@odata.context\":\"http://services.odata.org/OData/$metadata#Products\",\"@odata.count\":12,\"value\":[{\"Name\":\"Coffee\",\"Total\":2.55}]}", jsonResult);
        }

        [Fact]
        public void JsonSerializationWith_Context_ClassContent_Count_NextLink()
        {
            var item = new Thing();
            item.Name = "Coffee";
            item.Total = 2.55M;

            var responseContent = new ODataResponseContent(
                new Uri("http://services.odata.org/OData/$metadata#Products"),
                new[] { item },
                count: 12,
                nextLink: new Uri("http://services.odata.org/OData/Products?$skip=5"));

            var jsonResult = JsonConvert.SerializeObject(responseContent);

            Assert.Equal("{\"@odata.context\":\"http://services.odata.org/OData/$metadata#Products\",\"@odata.count\":12,\"@odata.nextLink\":\"http://services.odata.org/OData/Products?$skip=5\",\"value\":[{\"Name\":\"Coffee\",\"Total\":2.55}]}", jsonResult);
        }

        [Fact]
        public void JsonSerializationWith_Context_DynamicContent_Count()
        {
            dynamic item = new ExpandoObject();
            item.Id = 14225;
            item.Name = "Fred";

            var responseContent = new ODataResponseContent(
                new Uri("http://services.odata.org/OData/$metadata#Products"),
                new[] { item },
                count: 12);

            var jsonResult = JsonConvert.SerializeObject(responseContent);

            Assert.Equal("{\"@odata.context\":\"http://services.odata.org/OData/$metadata#Products\",\"@odata.count\":12,\"value\":[{\"Id\":14225,\"Name\":\"Fred\"}]}", jsonResult);
        }

        [Fact]
        public void JsonSerializationWith_Context_DynamicContent_Count_NextLink()
        {
            dynamic item = new ExpandoObject();
            item.Id = 14225;
            item.Name = "Fred";

            var responseContent = new ODataResponseContent(
                new Uri("http://services.odata.org/OData/$metadata#Products"),
                new[] { item },
                count: 12,
                nextLink: new Uri("http://services.odata.org/OData/Products?$skip=5"));

            var jsonResult = JsonConvert.SerializeObject(responseContent);

            Assert.Equal("{\"@odata.context\":\"http://services.odata.org/OData/$metadata#Products\",\"@odata.count\":12,\"@odata.nextLink\":\"http://services.odata.org/OData/Products?$skip=5\",\"value\":[{\"Id\":14225,\"Name\":\"Fred\"}]}", jsonResult);
        }

        [Fact]
        public void JsonSerializationWith_DynamicContent_Count()
        {
            dynamic item = new ExpandoObject();
            item.Id = 14225;
            item.Name = "Fred";

            var responseContent = new ODataResponseContent(
                null,
                new[] { item },
                count: 12);

            var jsonResult = JsonConvert.SerializeObject(responseContent);

            Assert.Equal("{\"@odata.count\":12,\"value\":[{\"Id\":14225,\"Name\":\"Fred\"}]}", jsonResult);
        }

        [Fact]
        public void JsonSerializationWith_SimpleContent_Count()
        {
            var responseContent = new ODataResponseContent(
                null,
                new[] { 1, 2, 3 },
                count: 5);

            var jsonResult = JsonConvert.SerializeObject(responseContent);

            Assert.Equal("{\"@odata.count\":5,\"value\":[1,2,3]}", jsonResult);
        }

        [DataContract]
        public class Thing
        {
            [DataMember]
            public string Name
            {
                get;
                set;
            }

            [DataMember]
            public decimal Total
            {
                get;
                set;
            }
        }
    }
}