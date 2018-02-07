namespace Net.Http.WebApi.OData.Tests
{
    using System;
    using NorthwindModel;
    using OData.Model;

    internal static class TestHelper
    {
        internal static void EnsureEDM()
        {
            var entityDataModelBuilder = new EntityDataModelBuilder(StringComparer.OrdinalIgnoreCase);
            entityDataModelBuilder.RegisterEntitySet<Category>("Categories", x => x.Name, Capabilities.Insertable | Capabilities.Updatable | Capabilities.Deletable);
            entityDataModelBuilder.RegisterEntitySet<Customer>("Customers", x => x.CompanyName, Capabilities.Updatable);
            entityDataModelBuilder.RegisterEntitySet<Employee>("Employees", x => x.Id);
            entityDataModelBuilder.RegisterEntitySet<Manager>("Managers", x => x.Id);
            entityDataModelBuilder.RegisterEntitySet<Order>("Orders", x => x.OrderId, Capabilities.Insertable | Capabilities.Updatable);
            entityDataModelBuilder.RegisterEntitySet<Product>("Products", x => x.ProductId, Capabilities.Insertable | Capabilities.Updatable);

            entityDataModelBuilder.BuildModel();
        }
    }
}