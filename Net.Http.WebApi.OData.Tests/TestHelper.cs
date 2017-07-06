namespace Net.Http.WebApi.OData.Tests
{
    using NorthwindModel;
    using OData.Model;

    internal static class TestHelper
    {
        internal static void EnsureEDM()
        {
            var entityDataModelBuilder = new EntityDataModelBuilder();
            entityDataModelBuilder.RegisterEntitySet<Category>("Categories", x => x.Name);
            entityDataModelBuilder.RegisterEntitySet<Customer>("Customers", x => x.CompanyName);
            entityDataModelBuilder.RegisterEntitySet<Employee>("Employees", x => x.EmailAddress);
            entityDataModelBuilder.RegisterEntitySet<Order>("Orders", x => x.OrderId);
            entityDataModelBuilder.RegisterEntitySet<Product>("Products", x => x.ProductId);

            entityDataModelBuilder.BuildModel();
        }
    }
}