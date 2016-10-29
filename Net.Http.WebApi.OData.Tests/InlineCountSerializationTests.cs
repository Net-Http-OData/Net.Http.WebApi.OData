namespace Net.Http.WebApi.Tests.OData
{
    using System.Dynamic;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Xml;
    using Net.Http.WebApi.OData;
    using Newtonsoft.Json;
    using Xunit;

    public class InlineCountSerializationTests
    {
        [Fact]
        public void JsonSerializationWithClassContent()
        {
            var item = new Thing();
            item.Name = "Coffee";
            item.Total = 2.55M;

            var inlineCount = new InlineCount<Thing>(new[] { item }, count: 12);

            var result = JsonConvert.SerializeObject(inlineCount);

            Assert.Equal("{\"__count\":12,\"results\":[{\"Name\":\"Coffee\",\"Total\":2.55}]}", result);
        }

        [Fact]
        public void JsonSerializationWithDynamicContent()
        {
            dynamic item = new ExpandoObject();
            item.Id = 14225;
            item.Name = "Fred";

            var inlineCount = new InlineCount<dynamic>(new[] { item }, count: 12);

            var result = JsonConvert.SerializeObject(inlineCount);

            Assert.Equal("{\"__count\":12,\"results\":[{\"Id\":14225,\"Name\":\"Fred\"}]}", result);
        }

        [Fact]
        public void JsonSerializationWithSimpleContent()
        {
            var inlineCount = new InlineCount<int>(new[] { 1, 2, 3 }, count: 5);

            var result = JsonConvert.SerializeObject(inlineCount);

            Assert.Equal("{\"__count\":5,\"results\":[1,2,3]}", result);
        }

        [Fact]
        public void XmlSerializationWithClassContent()
        {
            var item = new Thing();
            item.Name = "Coffee";
            item.Total = 2.55M;

            var inlineCount = new InlineCount<Thing>(new[] { item }, count: 12);

            var stringBuilder = new StringBuilder();
            using (var xmlWriter = XmlWriter.Create(stringBuilder, new XmlWriterSettings { Indent = false, OmitXmlDeclaration = true }))
            {
                var serializer = new DataContractSerializer(inlineCount.GetType());
                serializer.WriteObject(xmlWriter, inlineCount);
            }

            var result = stringBuilder.ToString();

            Assert.Equal("<InlineCountOfInlineCountSerializationTests.ThingAeZ_PiJ7K xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://schemas.datacontract.org/2004/07/Net.Http.WebApi.OData\"><__count>12</__count><results xmlns:d2p1=\"http://schemas.datacontract.org/2004/07/Net.Http.WebApi.Tests.OData\"><d2p1:InlineCountSerializationTests.Thing><d2p1:Name>Coffee</d2p1:Name><d2p1:Total>2.55</d2p1:Total></d2p1:InlineCountSerializationTests.Thing></results></InlineCountOfInlineCountSerializationTests.ThingAeZ_PiJ7K>", result);
        }

        [Fact]
        public void XmlSerializationWithSimpleContent()
        {
            var inlineCount = new InlineCount<int>(new[] { 1, 2, 3 }, count: 5);

            var stringBuilder = new StringBuilder();
            using (var xmlWriter = XmlWriter.Create(stringBuilder, new XmlWriterSettings { Indent = false, OmitXmlDeclaration = true }))
            {
                var serializer = new DataContractSerializer(inlineCount.GetType());
                serializer.WriteObject(xmlWriter, inlineCount);
            }

            var result = stringBuilder.ToString();

            Assert.Equal("<InlineCountOfint xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://schemas.datacontract.org/2004/07/Net.Http.WebApi.OData\"><__count>5</__count><results xmlns:d2p1=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\"><d2p1:int>1</d2p1:int><d2p1:int>2</d2p1:int><d2p1:int>3</d2p1:int></results></InlineCountOfint>", result);
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