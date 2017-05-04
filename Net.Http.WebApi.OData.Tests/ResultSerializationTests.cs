namespace Net.Http.WebApi.OData.Tests
{
    using System.Dynamic;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using Xunit;

    public class ResultSerializationTests
    {
        [Fact]
        public void JsonSerializationWithClassContent()
        {
            var item = new Thing();
            item.Name = "Coffee";
            item.Total = 2.55M;

            var result = new Result<Thing>(new[] { item });

            var jsonResult = JsonConvert.SerializeObject(result);

            Assert.Equal("{\"value\":[{\"Name\":\"Coffee\",\"Total\":2.55}]}", jsonResult);
        }

        [Fact]
        public void JsonSerializationWithDynamicContent()
        {
            dynamic item = new ExpandoObject();
            item.Id = 14225;
            item.Name = "Fred";

            var result = new Result<dynamic>(new[] { item });

            var jsonResult = JsonConvert.SerializeObject(result);

            Assert.Equal("{\"value\":[{\"Id\":14225,\"Name\":\"Fred\"}]}", jsonResult);
        }

        [Fact]
        public void JsonSerializationWithSimpleContent()
        {
            var result = new Result<int>(new[] { 1, 2, 3 });

            var jsonResult = JsonConvert.SerializeObject(result);

            Assert.Equal("{\"value\":[1,2,3]}", jsonResult);
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