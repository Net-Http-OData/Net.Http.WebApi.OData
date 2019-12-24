using System;
using Net.Http.WebApi.OData.Model;
using NorthwindModel;
using Xunit;

namespace Net.Http.WebApi.OData.Tests.Model
{
    public class EntityDataModelTests
    {
        private readonly EntityDataModel entityDataModel;

        public EntityDataModelTests()
        {
            var entityDataModelBuilder = new EntityDataModelBuilder(StringComparer.OrdinalIgnoreCase);
            entityDataModelBuilder.RegisterEntitySet<Category>("Categories", x => x.Name, Capabilities.Insertable | Capabilities.Updatable | Capabilities.Deletable);
            entityDataModelBuilder.RegisterEntitySet<Customer>("Customers", x => x.CompanyName, Capabilities.Updatable);
            entityDataModelBuilder.RegisterEntitySet<Employee>("Employees", x => x.Id);
            entityDataModelBuilder.RegisterEntitySet<Manager>("Managers", x => x.Id);
            entityDataModelBuilder.RegisterEntitySet<Order>("Orders", x => x.OrderId, Capabilities.Insertable | Capabilities.Updatable);
            entityDataModelBuilder.RegisterEntitySet<Product>("Products", x => x.ProductId, Capabilities.Insertable | Capabilities.Updatable);

            this.entityDataModel = entityDataModelBuilder.BuildModel();
        }

        [Fact]
        public void FilterFunctions_AreSet()
        {
            Assert.Equal(26, this.entityDataModel.FilterFunctions.Count);

            Assert.Contains("cast", this.entityDataModel.FilterFunctions);
            Assert.Contains("ceiling", this.entityDataModel.FilterFunctions);
            Assert.Contains("concat", this.entityDataModel.FilterFunctions);
            Assert.Contains("contains", this.entityDataModel.FilterFunctions);
            Assert.Contains("day", this.entityDataModel.FilterFunctions);
            Assert.Contains("endswith", this.entityDataModel.FilterFunctions);
            Assert.Contains("floor", this.entityDataModel.FilterFunctions);
            Assert.Contains("fractionalseconds", this.entityDataModel.FilterFunctions);
            Assert.Contains("hour", this.entityDataModel.FilterFunctions);
            Assert.Contains("indexof", this.entityDataModel.FilterFunctions);
            Assert.Contains("isof", this.entityDataModel.FilterFunctions);
            Assert.Contains("length", this.entityDataModel.FilterFunctions);
            Assert.Contains("maxdatetime", this.entityDataModel.FilterFunctions);
            Assert.Contains("mindatetime", this.entityDataModel.FilterFunctions);
            Assert.Contains("minute", this.entityDataModel.FilterFunctions);
            Assert.Contains("month", this.entityDataModel.FilterFunctions);
            Assert.Contains("now", this.entityDataModel.FilterFunctions);
            Assert.Contains("replace", this.entityDataModel.FilterFunctions);
            Assert.Contains("round", this.entityDataModel.FilterFunctions);
            Assert.Contains("second", this.entityDataModel.FilterFunctions);
            Assert.Contains("startswith", this.entityDataModel.FilterFunctions);
            Assert.Contains("substring", this.entityDataModel.FilterFunctions);
            Assert.Contains("tolower", this.entityDataModel.FilterFunctions);
            Assert.Contains("toupper", this.entityDataModel.FilterFunctions);
            Assert.Contains("trim", this.entityDataModel.FilterFunctions);
            Assert.Contains("year", this.entityDataModel.FilterFunctions);
        }

        [Fact]
        public void SupportedFormats_AreSet()
        {
            Assert.Equal(2, this.entityDataModel.SupportedFormats.Count);

            Assert.Contains("application/json;odata.metadata=none", this.entityDataModel.SupportedFormats);
            Assert.Contains("application/json;odata.metadata=minimal", this.entityDataModel.SupportedFormats);
        }
    }
}