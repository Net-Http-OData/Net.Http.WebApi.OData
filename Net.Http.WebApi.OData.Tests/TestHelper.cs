namespace Net.Http.WebApi.OData.Tests
{
    using NorthwindModel;
    using OData.Model;

    internal static class TestHelper
    {
        internal static void EnsureEDM()
        {
            var entityDataModelBuilder = new EntityDataModelBuilder();
            entityDataModelBuilder.RegisterEntitySet<Category>("Categories");
            entityDataModelBuilder.RegisterEntitySet<Customer>("Customers");
            entityDataModelBuilder.RegisterEntitySet<Employee>("Employees");
            entityDataModelBuilder.RegisterEntitySet<Order>("Orders");
            entityDataModelBuilder.RegisterEntitySet<Product>("Products");

            entityDataModelBuilder.BuildModel();
        }
    }
}