namespace Net.Http.WebApi.OData.Tests
{
    using NorthwindModel;
    using OData.Model;

    internal static class TestHelper
    {
        internal static void EnsureEDM()
        {
            var entityDataModelBuilder = new EntityDataModelBuilder();
            entityDataModelBuilder.RegisterCollection<Category>("Categories");
            entityDataModelBuilder.RegisterCollection<Customer>("Customers");
            entityDataModelBuilder.RegisterCollection<Employee>("Employees");
            entityDataModelBuilder.RegisterCollection<Order>("Orders");
            entityDataModelBuilder.RegisterCollection<Product>("Products");

            entityDataModelBuilder.BuildModel();
        }
    }
}