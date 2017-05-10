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
            entityDataModelBuilder.RegisterEntitySet<Category>("Categories", x => x.Name);

            var exception = Assert.Throws<ArgumentException>(() => entityDataModelBuilder.RegisterEntitySet<Category>("categories", x => x.Name));
            Assert.Equal("An item with the same key has already been added.", exception.Message);
        }

        [Fact]
        public void CollectionNamesCaseInsensitiveByDefault_ResolveCollectionVaryingCollectionNameCasing()
        {
            var entityDataModelBuilder = new EntityDataModelBuilder();
            entityDataModelBuilder.RegisterEntitySet<Category>("Categories", x => x.Name);

            var entityDataModel = entityDataModelBuilder.BuildModel();

            Assert.Same(entityDataModel.EntitySets["categories"], entityDataModel.EntitySets["Categories"]);
        }

        public class WhenCalling_BuildModelWith_Models
        {
            private readonly EntityDataModel entityDataModel;

            public WhenCalling_BuildModelWith_Models()
            {
                var entityDataModelBuilder = new EntityDataModelBuilder();
                entityDataModelBuilder.RegisterEntitySet<Category>("Categories", x => x.Name);
                entityDataModelBuilder.RegisterEntitySet<Customer>("Customers", x => x.CompanyName);
                entityDataModelBuilder.RegisterEntitySet<Employee>("Employees", x => x.EmailAddress);
                entityDataModelBuilder.RegisterEntitySet<Order>("Orders", x => x.OrderId);
                entityDataModelBuilder.RegisterEntitySet<Product>("Products", x => x.Name);

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
                Assert.Equal("NorthwindModel.Category", edmComplexType.FullName);
                Assert.Equal("Category", edmComplexType.Name);
                Assert.Equal(1, edmComplexType.Properties.Count);

                Assert.Same(edmComplexType, edmComplexType.Properties[0].DeclaringType);
                Assert.Equal("Name", edmComplexType.Properties[0].Name);
                Assert.Same(EdmPrimitiveType.String, edmComplexType.Properties[0].PropertyType);

                Assert.Same(edmComplexType.Properties[0], edmComplexType.EntityKey);
            }

            [Fact]
            public void The_Customers_CollectionIsCorrect()
            {
                Assert.True(this.entityDataModel.EntitySets.ContainsKey("Customers"));

                var edmComplexType = this.entityDataModel.EntitySets["Customers"];

                Assert.Equal(typeof(Customer), edmComplexType.ClrType);
                Assert.Equal("NorthwindModel.Customer", edmComplexType.FullName);
                Assert.Equal("Customer", edmComplexType.Name);
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

                Assert.Same(edmComplexType.Properties[1], edmComplexType.EntityKey);
            }

            [Fact]
            public void The_Employees_CollectionIsCorrect()
            {
                Assert.True(this.entityDataModel.EntitySets.ContainsKey("Employees"));

                var edmComplexType = this.entityDataModel.EntitySets["Employees"];

                Assert.Equal(typeof(Employee), edmComplexType.ClrType);
                Assert.Equal("NorthwindModel.Employee", edmComplexType.FullName);
                Assert.Equal("Employee", edmComplexType.Name);
                Assert.Equal(6, edmComplexType.Properties.Count);

                Assert.Same(edmComplexType, edmComplexType.Properties[0].DeclaringType);
                Assert.Equal("BirthDate", edmComplexType.Properties[0].Name);
                Assert.Same(EdmPrimitiveType.DateTime, edmComplexType.Properties[0].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[1].DeclaringType);
                Assert.Equal("EmailAddress", edmComplexType.Properties[1].Name);
                Assert.Same(EdmPrimitiveType.String, edmComplexType.Properties[1].PropertyType);

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

                Assert.Same(edmComplexType.Properties[1], edmComplexType.EntityKey);
            }

            [Fact]
            public void The_Orders_CollectionIsCorrect()
            {
                Assert.True(this.entityDataModel.EntitySets.ContainsKey("Orders"));

                var edmComplexType = this.entityDataModel.EntitySets["Orders"];

                Assert.Equal(typeof(Order), edmComplexType.ClrType);
                Assert.Equal("NorthwindModel.Order", edmComplexType.FullName);
                Assert.Equal("Order", edmComplexType.Name);
                Assert.Equal(4, edmComplexType.Properties.Count);

                Assert.Same(edmComplexType, edmComplexType.Properties[0].DeclaringType);
                Assert.Equal("Freight", edmComplexType.Properties[0].Name);
                Assert.Same(EdmPrimitiveType.Decimal, edmComplexType.Properties[0].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[1].DeclaringType);
                Assert.Equal("OrderId", edmComplexType.Properties[1].Name);
                Assert.Same(EdmPrimitiveType.Int64, edmComplexType.Properties[1].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[2].DeclaringType);
                Assert.Equal("ShipCountry", edmComplexType.Properties[2].Name);
                Assert.Same(EdmPrimitiveType.String, edmComplexType.Properties[2].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[3].DeclaringType);
                Assert.Equal("TransactionId", edmComplexType.Properties[3].Name);
                Assert.Same(EdmPrimitiveType.Guid, edmComplexType.Properties[3].PropertyType);

                Assert.Same(edmComplexType.Properties[1], edmComplexType.EntityKey);
            }

            [Fact]
            public void The_Products_CollectionIsCorrect()
            {
                Assert.True(this.entityDataModel.EntitySets.ContainsKey("Products"));

                var edmComplexType = this.entityDataModel.EntitySets["Products"];

                Assert.Equal(typeof(Product), edmComplexType.ClrType);
                Assert.Equal("NorthwindModel.Product", edmComplexType.FullName);
                Assert.Equal("Product", edmComplexType.Name);
                Assert.Equal(7, edmComplexType.Properties.Count);

                Assert.Same(edmComplexType, edmComplexType.Properties[0].DeclaringType);
                Assert.Equal("Category", edmComplexType.Properties[0].Name);
                Assert.Same(EdmType.GetEdmType(typeof(Category)), edmComplexType.Properties[0].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[1].DeclaringType);
                Assert.Equal("Deleted", edmComplexType.Properties[1].Name);
                Assert.Same(EdmPrimitiveType.Boolean, edmComplexType.Properties[1].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[2].DeclaringType);
                Assert.Equal("Description", edmComplexType.Properties[2].Name);
                Assert.Same(EdmPrimitiveType.String, edmComplexType.Properties[2].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[3].DeclaringType);
                Assert.Equal("Name", edmComplexType.Properties[3].Name);
                Assert.Same(EdmPrimitiveType.String, edmComplexType.Properties[3].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[4].DeclaringType);
                Assert.Equal("Price", edmComplexType.Properties[4].Name);
                Assert.Same(EdmPrimitiveType.Decimal, edmComplexType.Properties[4].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[5].DeclaringType);
                Assert.Equal("Rating", edmComplexType.Properties[5].Name);
                Assert.Same(EdmPrimitiveType.Int32, edmComplexType.Properties[5].PropertyType);

                Assert.Same(edmComplexType, edmComplexType.Properties[6].DeclaringType);
                Assert.Equal("ReleaseDate", edmComplexType.Properties[6].Name);
                Assert.Same(EdmPrimitiveType.DateTime, edmComplexType.Properties[6].PropertyType);

                Assert.Same(edmComplexType.Properties[3], edmComplexType.EntityKey);
            }

            [Fact]
            public void ThereAre_5_RegisteredCollections()
            {
                Assert.Equal(5, this.entityDataModel.EntitySets.Count);
            }
        }
    }
}