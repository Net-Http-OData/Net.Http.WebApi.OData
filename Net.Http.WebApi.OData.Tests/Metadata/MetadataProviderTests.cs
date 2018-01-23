namespace Net.Http.WebApi.OData.Tests.Metadata
{
    using Net.Http.WebApi.OData.Metadata;
    using Net.Http.WebApi.OData.Model;
    using Xunit;

    public class MetadataProviderTests
    {
        [Fact]
        public void Create_Returns_CSDL()
        {
            TestHelper.EnsureEDM();

            var csdlDocument = MetadataProvider.Create(EntityDataModel.Current);

            Assert.Equal(@"<edmx:Edmx Version=""4.0"" xmlns:edmx=""http://docs.oasis-open.org/odata/ns/edmx"">
  <edmx:DataServices>
    <Schema Namespace=""NorthwindModel"" xmlns=""http://docs.oasis-open.org/odata/ns/edm"">
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
        <Property Name=""OrderId"" Type=""Int64"" Nullable=""false"" />
        <Property Name=""ProductId"" Type=""Int32"" Nullable=""false"" />
        <Property Name=""Quantity"" Type=""Int16"" Nullable=""false"" />
        <Property Name=""UnitPrice"" Type=""Decimal"" Nullable=""false"" />
      </ComplexType>
      <EntityType Name=""Categories"">
        <Key>
          <PropertyRef Name=""Name"" />
        </Key>
        <Property Name=""Name"" Type=""String"" Nullable=""false"" />
      </EntityType>
      <EntityType Name=""Customers"">
        <Key>
          <PropertyRef Name=""CompanyName"" />
        </Key>
        <Property Name=""City"" Type=""String"" Nullable=""false"" />
        <Property Name=""CompanyName"" Type=""String"" Nullable=""false"" />
        <Property Name=""Country"" Type=""String"" Nullable=""false"" />
        <Property Name=""LegacyId"" Type=""Int32"" Nullable=""false"" />
      </EntityType>
      <EntityType Name=""Employees"">
        <Key>
          <PropertyRef Name=""EmailAddress"" />
        </Key>
        <Property Name=""AccessLevel"" Type=""AccessLevel"" Nullable=""false"" />
        <Property Name=""BirthDate"" Type=""Date"" Nullable=""false"" />
        <Property Name=""EmailAddress"" Type=""String"" Nullable=""false"" />
        <Property Name=""Forename"" Type=""String"" Nullable=""false"" />
        <Property Name=""ImageData"" Type=""String"" Nullable=""false"" />
        <Property Name=""Surname"" Type=""String"" Nullable=""false"" />
        <Property Name=""Title"" Type=""String"" Nullable=""false"" />
      </EntityType>
      <EntityType Name=""Orders"">
        <Key>
          <PropertyRef Name=""OrderId"" />
        </Key>
        <Property Name=""Freight"" Type=""Decimal"" Nullable=""false"" />
        <Property Name=""OrderDetails"" Type=""OrderDetail"" Nullable=""false"" />
        <Property Name=""OrderId"" Type=""Int64"" Nullable=""false"" />
        <Property Name=""ShipCountry"" Type=""String"" Nullable=""false"" />
        <Property Name=""TransactionId"" Type=""Guid"" Nullable=""false"" />
      </EntityType>
      <EntityType Name=""Products"">
        <Key>
          <PropertyRef Name=""ProductId"" />
        </Key>
        <Property Name=""Category"" Type=""Category"" Nullable=""false"" />
        <Property Name=""Colour"" Type=""Colour"" Nullable=""false"" />
        <Property Name=""Deleted"" Type=""Boolean"" Nullable=""false"" />
        <Property Name=""Description"" Type=""String"" Nullable=""false"" />
        <Property Name=""Name"" Type=""String"" Nullable=""false"" />
        <Property Name=""Price"" Type=""Decimal"" Nullable=""false"" />
        <Property Name=""ProductId"" Type=""Int32"" Nullable=""false"" />
        <Property Name=""Rating"" Type=""Int32"" Nullable=""false"" />
        <Property Name=""ReleaseDate"" Type=""Date"" Nullable=""false"" />
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
</edmx:Edmx>", csdlDocument.ToString());
        }
    }
}