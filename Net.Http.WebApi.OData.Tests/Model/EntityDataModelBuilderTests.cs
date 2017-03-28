namespace Net.Http.WebApi.OData.Tests.Model
{
    using System;
    using NorthwindModel;
    using OData.Model;
    using Xunit;

    public class EntityDataModelBuilderTests
    {
        [Fact]
        public void CollectionNamesCaseInsensitiveByDefault_RegisterCollectionThrowsArgumentExceptionForDuplicateKeyVaryingOnlyOnCasing()
        {
            var entityDataModelBuilder = new EntityDataModelBuilder();
            entityDataModelBuilder.RegisterEntitySet<Category>("Categories");

            var exception = Assert.Throws<ArgumentException>(() => entityDataModelBuilder.RegisterEntitySet<Category>("categories"));
            Assert.Equal("An item with the same key has already been added.", exception.Message);
        }

        [Fact]
        public void CollectionNamesCaseInsensitiveByDefault_ResolveCollectionVaryingCollectionNameCasing()
        {
            var entityDataModelBuilder = new EntityDataModelBuilder();
            entityDataModelBuilder.RegisterEntitySet<Category>("Categories");

            var entityDataModel = entityDataModelBuilder.BuildModel();

            Assert.Same(entityDataModel.EntitySets["categories"], entityDataModel.EntitySets["Categories"]);
        }

        public class WhenCalling_BuildModelWith_Models
        {
            private readonly EntityDataModel entityDataModel;

            public WhenCalling_BuildModelWith_Models()
            {
                var entityDataModelBuilder = new EntityDataModelBuilder();
                entityDataModelBuilder.RegisterEntitySet<Category>("Categories");
                entityDataModelBuilder.RegisterEntitySet<Customer>("Customers");
                entityDataModelBuilder.RegisterEntitySet<Employee>("Employees");
                entityDataModelBuilder.RegisterEntitySet<Order>("Orders");
                entityDataModelBuilder.RegisterEntitySet<Product>("Products");

                this.entityDataModel = entityDataModelBuilder.BuildModel();
            }

            [Fact]
            public void EntityDataModel_Current_IsSetToTheReturnedModel()
            {
                Assert.Same(this.entityDataModel, EntityDataModel.Current);
            }

            [Fact]
            public void The_Categories_CollectionIsCorrect()
            {
                Assert.True(this.entityDataModel.EntitySets.ContainsKey("Categories"));

                var edmComplexType = this.entityDataModel.EntitySets["Categories"];

                Assert.Equal(typeof(Category), edmComplexType.ClrType);
                Assert.Equal("NorthwindModel.Category", edmComplexType.Name);
                Assert.Equal(1, edmComplexType.Properties.Count);

                Assert.Same(edmComplexType, edmComplexType.Properties[0].DeclaringType);
                Assert.Equal("Name", edmComplexType.Properties[0].Name);
                Assert.Same(EdmPrimitiveType.String, edmComplexType.Properties[0].PropertyType);
            }

            [Fact]
            public void The_Customers_CollectionIsCorrect()
            {
                Assert.True(this.entityDataModel.EntitySets.ContainsKey("Customers"));

                var edmComplexType = this.entityDataModel.EntitySets["Customers"];

                Assert.Equal(typeof(Customer), edmComplexType.ClrType);
                Assert.Equal("NorthwindModel.Customer", edmComplexType.Name);
                Assert.Equal(4, edmComplexType.Properties.Count);

                Assert.Same(edmComplexType, edmComplexType.Properties[0].DeclaringType);
                Assert.Equal("City", edmComplexType.Properties[0].Name);
                Assert.Same(EdmPrimitiveType.String, edmComplexType.Properties[0].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[1].DeclaringType);
                Assert.Equal("CompanyName", edmComplexType.Properties[1].Name);
                Assert.Same(EdmPrimitiveType.String, edmComplexType.Properties[1].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[2].DeclaringType);
                Assert.Equal("Country", edmComplexType.Properties[2].Name);
                Assert.Same(EdmPrimitiveType.String, edmComplexType.Properties[2].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[3].DeclaringType);
                Assert.Equal("LegacyId", edmComplexType.Properties[3].Name);
                Assert.Same(EdmPrimitiveType.Int32, edmComplexType.Properties[3].PropertyType);
            }

            [Fact]
            public void The_Employees_CollectionIsCorrect()
            {
                Assert.True(this.entityDataModel.EntitySets.ContainsKey("Employees"));

                var edmComplexType = this.entityDataModel.EntitySets["Employees"];

                Assert.Equal(typeof(Employee), edmComplexType.ClrType);
                Assert.Equal("NorthwindModel.Employee", edmComplexType.Name);
                Assert.Equal(6, edmComplexType.Properties.Count);

                Assert.Same(edmComplexType, edmComplexType.Properties[0].DeclaringType);
                Assert.Equal("AccessLevel", edmComplexType.Properties[0].Name);
                Assert.Same(EdmType.GetEdmType(typeof(AccessLevel)), edmComplexType.Properties[0].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[1].DeclaringType);
                Assert.Equal("BirthDate", edmComplexType.Properties[1].Name);
                Assert.Same(EdmPrimitiveType.Date, edmComplexType.Properties[1].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[2].DeclaringType);
                Assert.Equal("Forename", edmComplexType.Properties[2].Name);
                Assert.Same(EdmPrimitiveType.String, edmComplexType.Properties[2].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[3].DeclaringType);
                Assert.Equal("ImageData", edmComplexType.Properties[3].Name);
                Assert.Same(EdmPrimitiveType.String, edmComplexType.Properties[3].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[4].DeclaringType);
                Assert.Equal("Surname", edmComplexType.Properties[4].Name);
                Assert.Same(EdmPrimitiveType.String, edmComplexType.Properties[4].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[5].DeclaringType);
                Assert.Equal("Title", edmComplexType.Properties[5].Name);
                Assert.Same(EdmPrimitiveType.String, edmComplexType.Properties[5].PropertyType);
            }

            [Fact]
            public void The_Orders_CollectionIsCorrect()
            {
                Assert.True(this.entityDataModel.EntitySets.ContainsKey("Orders"));

                var edmComplexType = this.entityDataModel.EntitySets["Orders"];

                Assert.Equal(typeof(Order), edmComplexType.ClrType);
                Assert.Equal("NorthwindModel.Order", edmComplexType.Name);
                Assert.Equal(3, edmComplexType.Properties.Count);

                Assert.Same(edmComplexType, edmComplexType.Properties[0].DeclaringType);
                Assert.Equal("Freight", edmComplexType.Properties[0].Name);
                Assert.Same(EdmPrimitiveType.Decimal, edmComplexType.Properties[0].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[1].DeclaringType);
                Assert.Equal("ShipCountry", edmComplexType.Properties[1].Name);
                Assert.Same(EdmPrimitiveType.String, edmComplexType.Properties[1].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[2].DeclaringType);
                Assert.Equal("TransactionId", edmComplexType.Properties[2].Name);
                Assert.Same(EdmPrimitiveType.Guid, edmComplexType.Properties[2].PropertyType);
            }

            [Fact]
            public void The_Products_CollectionIsCorrect()
            {
                Assert.True(this.entityDataModel.EntitySets.ContainsKey("Products"));

                var edmComplexType = this.entityDataModel.EntitySets["Products"];

                Assert.Equal(typeof(Product), edmComplexType.ClrType);
                Assert.Equal("NorthwindModel.Product", edmComplexType.Name);
                Assert.Equal(8, edmComplexType.Properties.Count);

                Assert.Same(edmComplexType, edmComplexType.Properties[0].DeclaringType);
                Assert.Equal("Category", edmComplexType.Properties[0].Name);
                Assert.Same(EdmType.GetEdmType(typeof(Category)), edmComplexType.Properties[0].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[1].DeclaringType);
                Assert.Equal("Colour", edmComplexType.Properties[1].Name);
                Assert.Same(EdmType.GetEdmType(typeof(Colour)), edmComplexType.Properties[1].PropertyType);

                var edmEnumType = (EdmEnumType)EdmType.GetEdmType(typeof(Colour));
                Assert.Equal(typeof(Colour), edmEnumType.ClrType);
                Assert.Equal("NorthwindModel.Colour", edmEnumType.Name);
                Assert.Equal(3, edmEnumType.Members.Count);
                Assert.Equal("Green", edmEnumType.Members[0].Name);
                Assert.Equal(1, edmEnumType.Members[0].Value);
                Assert.Equal("Blue", edmEnumType.Members[1].Name);
                Assert.Equal(2, edmEnumType.Members[1].Value);
                Assert.Equal("Red", edmEnumType.Members[2].Name);
                Assert.Equal(3, edmEnumType.Members[2].Value);

                Assert.Same(edmComplexType, edmComplexType.Properties[2].DeclaringType);
                Assert.Equal("Deleted", edmComplexType.Properties[2].Name);
                Assert.Same(EdmPrimitiveType.Boolean, edmComplexType.Properties[2].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[3].DeclaringType);
                Assert.Equal("Description", edmComplexType.Properties[3].Name);
                Assert.Same(EdmPrimitiveType.String, edmComplexType.Properties[3].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[4].DeclaringType);
                Assert.Equal("Name", edmComplexType.Properties[4].Name);
                Assert.Same(EdmPrimitiveType.String, edmComplexType.Properties[4].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[5].DeclaringType);
                Assert.Equal("Price", edmComplexType.Properties[5].Name);
                Assert.Same(EdmPrimitiveType.Decimal, edmComplexType.Properties[5].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[6].DeclaringType);
                Assert.Equal("Rating", edmComplexType.Properties[6].Name);
                Assert.Same(EdmPrimitiveType.Int32, edmComplexType.Properties[6].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[7].DeclaringType);
                Assert.Equal("ReleaseDate", edmComplexType.Properties[7].Name);
                Assert.Same(EdmPrimitiveType.Date, edmComplexType.Properties[7].PropertyType);
            }

            [Fact]
            public void ThereAre_5_RegisteredCollections()
            {
                Assert.Equal(5, this.entityDataModel.EntitySets.Count);
            }
        }
    }
}