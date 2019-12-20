namespace Net.Http.WebApi.OData.Tests.Metadata
{
    using System.Xml.Linq;
    using Net.Http.WebApi.OData.Metadata;
    using Net.Http.WebApi.OData.Model;
    using Xunit;

    public class MetadataProviderTests
    {
        [Fact]
        public void Create_Returns_CSDL()
        {
            TestHelper.EnsureEDM();

            var expected = XDocument.Parse(@"<edmx:Edmx xmlns:edmx=""http://docs.oasis-open.org/odata/ns/edmx"" Version=""4.0"">
  <edmx:DataServices>
    <Schema xmlns=""http://docs.oasis-open.org/odata/ns/edm"" Namespace=""NorthwindModel"">
      <EnumType Name=""AccessLevel"" UnderlyingType=""Edm.Int32"" IsFlags=""True"">
        <Member Name=""None"" Value=""0"" />
        <Member Name=""Read"" Value=""1"" />
        <Member Name=""Write"" Value=""2"" />
        <Member Name=""Delete"" Value=""4"" />
      </EnumType>
      <EnumType Name=""Colour"" UnderlyingType=""Edm.Int32"" IsFlags=""False"">
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
      </EntityType>
      <EntityType Name=""Managers"" BaseType=""NorthwindModel.Employee"">
        <Property Name=""AnnualBudget"" Type=""Edm.Decimal"" Nullable=""false"" />
        <Property Name=""Employees"" Type=""Collection(NorthwindModel.Employee)"" />
      </EntityType>
      <EntityType Name=""Orders"">
        <Key>
          <PropertyRef Name=""OrderId"" />
        </Key>
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
              <PropertyValue Property=""Insertable"" Bool=""True"" />
            </Record>
          </Annotation>
          <Annotation Term=""Org.OData.Capabilities.V1.UpdateRestrictions"">
            <Record>
              <PropertyValue Property=""Updatable"" Bool=""True"" />
            </Record>
          </Annotation>
          <Annotation Term=""Org.OData.Capabilities.V1.DeleteRestrictions"">
            <Record>
              <PropertyValue Property=""Deletable"" Bool=""True"" />
            </Record>
          </Annotation>
        </EntitySet>
        <EntitySet Name=""Customers"" EntityType=""NorthwindModel.Customer"">
          <Annotation Term=""Org.OData.Core.V1.ResourcePath"" String=""Customers"" />
          <Annotation Term=""Org.OData.Capabilities.V1.InsertRestrictions"">
            <Record>
              <PropertyValue Property=""Insertable"" Bool=""False"" />
            </Record>
          </Annotation>
          <Annotation Term=""Org.OData.Capabilities.V1.UpdateRestrictions"">
            <Record>
              <PropertyValue Property=""Updatable"" Bool=""True"" />
            </Record>
          </Annotation>
          <Annotation Term=""Org.OData.Capabilities.V1.DeleteRestrictions"">
            <Record>
              <PropertyValue Property=""Deletable"" Bool=""False"" />
            </Record>
          </Annotation>
        </EntitySet>
        <EntitySet Name=""Employees"" EntityType=""NorthwindModel.Employee"">
          <Annotation Term=""Org.OData.Core.V1.ResourcePath"" String=""Employees"" />
          <Annotation Term=""Org.OData.Capabilities.V1.InsertRestrictions"">
            <Record>
              <PropertyValue Property=""Insertable"" Bool=""False"" />
            </Record>
          </Annotation>
          <Annotation Term=""Org.OData.Capabilities.V1.UpdateRestrictions"">
            <Record>
              <PropertyValue Property=""Updatable"" Bool=""False"" />
            </Record>
          </Annotation>
          <Annotation Term=""Org.OData.Capabilities.V1.DeleteRestrictions"">
            <Record>
              <PropertyValue Property=""Deletable"" Bool=""False"" />
            </Record>
          </Annotation>
        </EntitySet>
        <EntitySet Name=""Managers"" EntityType=""NorthwindModel.Manager"">
          <Annotation Term=""Org.OData.Core.V1.ResourcePath"" String=""Managers"" />
          <Annotation Term=""Org.OData.Capabilities.V1.InsertRestrictions"">
            <Record>
              <PropertyValue Property=""Insertable"" Bool=""False"" />
            </Record>
          </Annotation>
          <Annotation Term=""Org.OData.Capabilities.V1.UpdateRestrictions"">
            <Record>
              <PropertyValue Property=""Updatable"" Bool=""False"" />
            </Record>
          </Annotation>
          <Annotation Term=""Org.OData.Capabilities.V1.DeleteRestrictions"">
            <Record>
              <PropertyValue Property=""Deletable"" Bool=""False"" />
            </Record>
          </Annotation>
        </EntitySet>
        <EntitySet Name=""Orders"" EntityType=""NorthwindModel.Order"">
          <Annotation Term=""Org.OData.Core.V1.ResourcePath"" String=""Orders"" />
          <Annotation Term=""Org.OData.Capabilities.V1.InsertRestrictions"">
            <Record>
              <PropertyValue Property=""Insertable"" Bool=""True"" />
            </Record>
          </Annotation>
          <Annotation Term=""Org.OData.Capabilities.V1.UpdateRestrictions"">
            <Record>
              <PropertyValue Property=""Updatable"" Bool=""True"" />
            </Record>
          </Annotation>
          <Annotation Term=""Org.OData.Capabilities.V1.DeleteRestrictions"">
            <Record>
              <PropertyValue Property=""Deletable"" Bool=""True"" />
            </Record>
          </Annotation>
        </EntitySet>
        <EntitySet Name=""Products"" EntityType=""NorthwindModel.Product"">
          <Annotation Term=""Org.OData.Core.V1.ResourcePath"" String=""Products"" />
          <Annotation Term=""Org.OData.Capabilities.V1.InsertRestrictions"">
            <Record>
              <PropertyValue Property=""Insertable"" Bool=""True"" />
            </Record>
          </Annotation>
          <Annotation Term=""Org.OData.Capabilities.V1.UpdateRestrictions"">
            <Record>
              <PropertyValue Property=""Updatable"" Bool=""True"" />
            </Record>
          </Annotation>
          <Annotation Term=""Org.OData.Capabilities.V1.DeleteRestrictions"">
            <Record>
              <PropertyValue Property=""Deletable"" Bool=""True"" />
            </Record>
          </Annotation>
        </EntitySet>
      </EntityContainer>
      <Annotations Target=""NorthwindModel.DefaultContainer"">
        <Annotation Term=""Org.OData.Capabilities.V1.DereferenceableIDs"" Bool=""true"" />
        <Annotation Term=""Org.OData.Capabilities.V1.ConventionalIDs"" Bool=""true"" />
        <Annotation Term=""Org.OData.Capabilities.V1.ConformanceLevel"">
          <EnumMember>Org.OData.Capabilities.V1.ConformanceLevelType/Minimal</EnumMember>
        </Annotation>
        <Annotation Term=""Org.OData.Capabilities.V1.SupportedFormats"">
          <Collection>
            <String>application/json;odata.metadata=none</String>
            <String>application/json;odata.metadata=minimal</String>
          </Collection>
        </Annotation>
        <Annotation Term=""Org.OData.Capabilities.V1.AsynchronousRequestsSupported"" Bool=""false"" />
        <Annotation Term=""Org.OData.Capabilities.V1.BatchContinueOnErrorSupported"" Bool=""false"" />
        <Annotation Term=""Org.OData.Capabilities.V1.FilterFunctions"">
          <Collection>
            <String>contains</String>
            <String>endswith</String>
            <String>startswith</String>
            <String>length</String>
            <String>indexof</String>
            <String>substring</String>
            <String>tolower</String>
            <String>toupper</String>
            <String>trim</String>
            <String>concat</String>
            <String>year</String>
            <String>month</String>
            <String>day</String>
            <String>hour</String>
            <String>second</String>
            <String>round</String>
            <String>floor</String>
            <String>ceiling</String>
            <String>cast</String>
            <String>isof</String>
          </Collection>
        </Annotation>
      </Annotations>
    </Schema>
  </edmx:DataServices>
</edmx:Edmx>");

            var csdlDocument = MetadataProvider.Create(EntityDataModel.Current);

            Assert.Equal(expected.ToString(), csdlDocument.ToString());
        }
    }
}