﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using Net.Http.OData;
using Xunit;

namespace Net.Http.WebApi.OData.Tests.Integration
{
    public class GetMetadataTests : IntegrationTest
    {
        private readonly HttpResponseMessage _response;

        public GetMetadataTests()
            => _response = HttpClient.GetAsync("http://server/odata/$metadata").Result;

        [Fact]
        [Trait("Category", "Integration")]
        public void Contains_Header_ContentType_ApplicationXml()
            => Assert.Equal("application/xml", _response.Content.Headers.ContentType.MediaType);

        [Fact]
        [Trait("Category", "Integration")]
        public void Contains_Header_ODataVersion()
            => Assert.Equal(ODataVersion.MaxVersion.ToString(), _response.Headers.GetValues(ODataResponseHeaderNames.ODataVersion).Single());

        [Fact]
        [Trait("Category", "Integration")]
        public async Task Contains_ODataResponseContent()
        {
            Assert.NotNull(_response.Content);

            string result = await _response.Content.ReadAsStringAsync();

            Assert.Equal(
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine + XDocument.Parse($@"<edmx:Edmx xmlns:edmx=""http://docs.oasis-open.org/odata/ns/edmx"" Version=""{ODataVersion.MaxVersion}"">
  <edmx:DataServices>
    <Schema xmlns=""http://docs.oasis-open.org/odata/ns/edm"" Namespace=""NorthwindModel"">
      <EnumType Name=""AccessLevel"" UnderlyingType=""Edm.Int32"" IsFlags=""true"">
        <Member Name=""None"" Value=""0"" />
        <Member Name=""Read"" Value=""1"" />
        <Member Name=""Write"" Value=""2"" />
        <Member Name=""Delete"" Value=""4"" />
      </EnumType>
      <EnumType Name=""Colour"" UnderlyingType=""Edm.Int32"" IsFlags=""false"">
        <Member Name=""Green"" Value=""1"" />
        <Member Name=""Blue"" Value=""2"" />
        <Member Name=""Red"" Value=""3"" />
      </EnumType>
      <ComplexType Name=""OrderDetail"">
        <Property Name=""OrderId"" Type=""Edm.Int64"" Nullable=""false"" />
        <Property Name=""ProductId"" Type=""Edm.Int32"" Nullable=""false"" />
        <Property Name=""Quantity"" Type=""Edm.Int16"" Nullable=""false"" />
        <Property Name=""UnitPrice"" Type=""Edm.Decimal"" Nullable=""false"" />
        <NavigationProperty Name=""Order"" Type=""NorthwindModel.Order"" />
      </ComplexType>
      <EntityType Name=""Categories"">
        <Key>
          <PropertyRef Name=""Name"" />
        </Key>
        <Property Name=""Name"" Type=""Edm.String"" />
      </EntityType>
      <EntityType Name=""Customers"">
        <Key>
          <PropertyRef Name=""CompanyName"" />
        </Key>
        <Property Name=""City"" Type=""Edm.String"" />
        <Property Name=""CompanyName"" Type=""Edm.String"" />
        <Property Name=""Country"" Type=""Edm.String"" />
        <Property Name=""LegacyId"" Type=""Edm.Int32"" Nullable=""false"" />
      </EntityType>
      <EntityType Name=""Employees"">
        <Key>
          <PropertyRef Name=""Id"" />
        </Key>
        <Property Name=""AccessLevel"" Type=""NorthwindModel.AccessLevel"" Nullable=""false"" />
        <Property Name=""BirthDate"" Type=""Edm.Date"" Nullable=""false"" />
        <Property Name=""EmailAddress"" Type=""Edm.String"" Nullable=""false"" />
        <Property Name=""Forename"" Type=""Edm.String"" Nullable=""false"" />
        <Property Name=""Id"" Type=""Edm.String"" Nullable=""false"" />
        <Property Name=""ImageData"" Type=""Edm.String"" />
        <Property Name=""JoiningDate"" Type=""Edm.Date"" Nullable=""false"" />
        <Property Name=""LeavingDate"" Type=""Edm.Date"" />
        <Property Name=""Surname"" Type=""Edm.String"" Nullable=""false"" />
        <Property Name=""Title"" Type=""Edm.String"" Nullable=""false"" />
        <NavigationProperty Name=""Manager"" Type=""NorthwindModel.Manager"" />
      </EntityType>
      <EntityType Name=""Managers"" BaseType=""NorthwindModel.Employee"">
        <Property Name=""AnnualBudget"" Type=""Edm.Decimal"" Nullable=""false"" />
        <NavigationProperty Name=""Employees"" Type=""Collection(NorthwindModel.Employee)"" />
      </EntityType>
      <EntityType Name=""Orders"">
        <Key>
          <PropertyRef Name=""OrderId"" />
        </Key>
        <Property Name=""Date"" Type=""Edm.DateTimeOffset"" Nullable=""false"" />
        <Property Name=""Freight"" Type=""Edm.Decimal"" Nullable=""false"" />
        <Property Name=""OrderDetails"" Type=""Collection(NorthwindModel.OrderDetail)"" />
        <Property Name=""OrderId"" Type=""Edm.Int64"" Nullable=""false"" />
        <Property Name=""ShipCountry"" Type=""Edm.String"" />
        <Property Name=""TransactionId"" Type=""Edm.Guid"" Nullable=""false"" />
      </EntityType>
      <EntityType Name=""Products"">
        <Key>
          <PropertyRef Name=""ProductId"" />
        </Key>
        <Property Name=""Colour"" Type=""NorthwindModel.Colour"" Nullable=""false"" />
        <Property Name=""Deleted"" Type=""Edm.Boolean"" Nullable=""false"" />
        <Property Name=""Description"" Type=""Edm.String"" />
        <Property Name=""Name"" Type=""Edm.String"" />
        <Property Name=""Price"" Type=""Edm.Decimal"" Nullable=""false"" />
        <Property Name=""ProductId"" Type=""Edm.Int32"" Nullable=""false"" />
        <Property Name=""Rating"" Type=""Edm.Int32"" Nullable=""false"" />
        <Property Name=""ReleaseDate"" Type=""Edm.Date"" Nullable=""false"" />
        <NavigationProperty Name=""Category"" Type=""NorthwindModel.Category"" />
      </EntityType>
      <EntityContainer Name=""DefaultContainer"">
        <EntitySet Name=""Categories"" EntityType=""NorthwindModel.Category"">
          <Annotation Term=""Org.OData.Core.V1.ResourcePath"" String=""Categories"" />
          <Annotation Term=""Org.OData.Capabilities.V1.InsertRestrictions"">
            <Record>
              <PropertyValue Property=""Insertable"" Bool=""true"" />
            </Record>
          </Annotation>
          <Annotation Term=""Org.OData.Capabilities.V1.UpdateRestrictions"">
            <Record>
              <PropertyValue Property=""Updatable"" Bool=""true"" />
            </Record>
          </Annotation>
          <Annotation Term=""Org.OData.Capabilities.V1.DeleteRestrictions"">
            <Record>
              <PropertyValue Property=""Deletable"" Bool=""true"" />
            </Record>
          </Annotation>
        </EntitySet>
        <EntitySet Name=""Customers"" EntityType=""NorthwindModel.Customer"">
          <Annotation Term=""Org.OData.Core.V1.ResourcePath"" String=""Customers"" />
          <Annotation Term=""Org.OData.Capabilities.V1.InsertRestrictions"">
            <Record>
              <PropertyValue Property=""Insertable"" Bool=""false"" />
            </Record>
          </Annotation>
          <Annotation Term=""Org.OData.Capabilities.V1.UpdateRestrictions"">
            <Record>
              <PropertyValue Property=""Updatable"" Bool=""true"" />
            </Record>
          </Annotation>
          <Annotation Term=""Org.OData.Capabilities.V1.DeleteRestrictions"">
            <Record>
              <PropertyValue Property=""Deletable"" Bool=""false"" />
            </Record>
          </Annotation>
        </EntitySet>
        <EntitySet Name=""Employees"" EntityType=""NorthwindModel.Employee"">
          <Annotation Term=""Org.OData.Core.V1.ResourcePath"" String=""Employees"" />
          <Annotation Term=""Org.OData.Capabilities.V1.InsertRestrictions"">
            <Record>
              <PropertyValue Property=""Insertable"" Bool=""false"" />
            </Record>
          </Annotation>
          <Annotation Term=""Org.OData.Capabilities.V1.UpdateRestrictions"">
            <Record>
              <PropertyValue Property=""Updatable"" Bool=""false"" />
            </Record>
          </Annotation>
          <Annotation Term=""Org.OData.Capabilities.V1.DeleteRestrictions"">
            <Record>
              <PropertyValue Property=""Deletable"" Bool=""false"" />
            </Record>
          </Annotation>
        </EntitySet>
        <EntitySet Name=""Managers"" EntityType=""NorthwindModel.Manager"">
          <Annotation Term=""Org.OData.Core.V1.ResourcePath"" String=""Managers"" />
          <Annotation Term=""Org.OData.Capabilities.V1.InsertRestrictions"">
            <Record>
              <PropertyValue Property=""Insertable"" Bool=""false"" />
            </Record>
          </Annotation>
          <Annotation Term=""Org.OData.Capabilities.V1.UpdateRestrictions"">
            <Record>
              <PropertyValue Property=""Updatable"" Bool=""false"" />
            </Record>
          </Annotation>
          <Annotation Term=""Org.OData.Capabilities.V1.DeleteRestrictions"">
            <Record>
              <PropertyValue Property=""Deletable"" Bool=""false"" />
            </Record>
          </Annotation>
        </EntitySet>
        <EntitySet Name=""Orders"" EntityType=""NorthwindModel.Order"">
          <Annotation Term=""Org.OData.Core.V1.ResourcePath"" String=""Orders"" />
          <Annotation Term=""Org.OData.Capabilities.V1.InsertRestrictions"">
            <Record>
              <PropertyValue Property=""Insertable"" Bool=""true"" />
            </Record>
          </Annotation>
          <Annotation Term=""Org.OData.Capabilities.V1.UpdateRestrictions"">
            <Record>
              <PropertyValue Property=""Updatable"" Bool=""true"" />
            </Record>
          </Annotation>
          <Annotation Term=""Org.OData.Capabilities.V1.DeleteRestrictions"">
            <Record>
              <PropertyValue Property=""Deletable"" Bool=""true"" />
            </Record>
          </Annotation>
        </EntitySet>
        <EntitySet Name=""Products"" EntityType=""NorthwindModel.Product"">
          <Annotation Term=""Org.OData.Core.V1.ResourcePath"" String=""Products"" />
          <Annotation Term=""Org.OData.Capabilities.V1.InsertRestrictions"">
            <Record>
              <PropertyValue Property=""Insertable"" Bool=""true"" />
            </Record>
          </Annotation>
          <Annotation Term=""Org.OData.Capabilities.V1.UpdateRestrictions"">
            <Record>
              <PropertyValue Property=""Updatable"" Bool=""true"" />
            </Record>
          </Annotation>
          <Annotation Term=""Org.OData.Capabilities.V1.DeleteRestrictions"">
            <Record>
              <PropertyValue Property=""Deletable"" Bool=""true"" />
            </Record>
          </Annotation>
        </EntitySet>
      </EntityContainer>
      <Annotations Target=""NorthwindModel.DefaultContainer"">
        <Annotation Term=""Org.OData.Capabilities.V1.ConformanceLevel"">
          <EnumMember>Org.OData.Capabilities.V1.ConformanceLevelType/Minimal</EnumMember>
        </Annotation>
        <Annotation Term=""Org.OData.Capabilities.V1.SupportedFormats"">
          <Collection>
            <String>application/json;odata.metadata=none</String>
            <String>application/json;odata.metadata=minimal</String>
          </Collection>
        </Annotation>
        <Annotation Term=""Org.OData.Capabilities.V1.SupportedMetadataFormats"">
          <Collection>
            <String>application/json</String>
            <String>text/plain</String>
          </Collection>
        </Annotation>
        <Annotation Term=""Org.OData.Capabilities.V1.AsynchronousRequestsSupported"" Bool=""false"" />
        <Annotation Term=""Org.OData.Capabilities.V1.BatchContinueOnErrorSupported"" Bool=""false"" />
        <Annotation Term=""Org.OData.Capabilities.V1.CrossJoinSupported"" Bool=""false"" />
        <Annotation Term=""Org.OData.Capabilities.V1.IndexableByKey"" Bool=""true"" />
        <Annotation Term=""Org.OData.Capabilities.V1.TopSupported"" Bool=""true"" />
        <Annotation Term=""Org.OData.Capabilities.V1.SkipSupported"" Bool=""true"" />
        <Annotation Term=""Org.OData.Capabilities.V1.ComputeSupported"" Bool=""false"" />
        <Annotation Term=""Org.OData.Capabilities.V1.BatchSupported"" Bool=""false"" />
        <Annotation Term=""Org.OData.Capabilities.V1.FilterFunctions"">
          <Collection>
            <String>concat</String>
            <String>contains</String>
            <String>endswith</String>
            <String>indexof</String>
            <String>length</String>
            <String>startswith</String>
            <String>substring</String>
            <String>tolower</String>
            <String>toupper</String>
            <String>trim</String>
            <String>date</String>
            <String>day</String>
            <String>fractionalseconds</String>
            <String>hour</String>
            <String>maxdatetime</String>
            <String>mindatetime</String>
            <String>minute</String>
            <String>month</String>
            <String>now</String>
            <String>second</String>
            <String>totaloffsetminutes</String>
            <String>year</String>
            <String>ceiling</String>
            <String>floor</String>
            <String>round</String>
            <String>cast</String>
            <String>isof</String>
          </Collection>
        </Annotation>
        <Annotation Term=""Org.OData.Capabilities.V1.KeyAsSegmentSupported"" Bool=""false"" />
        <Annotation Term=""Org.OData.Capabilities.V1.QuerySegmentSupported"" Bool=""false"" />
        <Annotation Term=""Org.OData.Capabilities.V1.AnnotationValuesInQuerySupported"" Bool=""false"" />
        <Annotation Term=""Org.OData.Capabilities.V1.MediaLocationUpdateSupported"" Bool=""false"" />
      </Annotations>
    </Schema>
  </edmx:DataServices>
</edmx:Edmx>").ToString(),
                result);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Not_Contains_Header_ContentType_Parameter_ODataMetadata()
            => Assert.DoesNotContain(_response.Content.Headers.ContentType.Parameters, x => x.Name == ODataMetadataLevelExtensions.HeaderName);

        [Fact]
        [Trait("Category", "Integration")]
        public void StatusCode_OK()
            => Assert.Equal(HttpStatusCode.OK, _response.StatusCode);
    }
}
