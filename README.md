Net.Http.WebApi.OData
=====================

[![NuGet version](https://badge.fury.io/nu/Net.Http.WebApi.OData.svg)](http://badge.fury.io/nu/Net.Http.WebApi.OData) [![Build Status](https://dev.azure.com/trevorpilley/Net.Http.WebApi.OData/_apis/build/status/Net.Http.WebApi.OData-CI?branchName=develop)](https://dev.azure.com/trevorpilley/Net.Http.WebApi.OData/_build/latest?definitionId=1&branchName=develop)

Net.Http.WebApi.OData is a C# library which parses an OData 4.0 query uri into an object model which can be used to query custom data sources which are not IQueryable. It was extracted from the [MicroLite.Extensions.WebApi](https://github.com/TrevorPilley/MicroLite.Extensions.WebApi) library into a separate project so that it could be easily used by others.

## Installation

To use it in your own Web API you need to install the nuget package `Install-Package Net.Http.WebApi.OData`

## Configuration

Somewhere in your application startup/webapi configuration, specify the types which will be used for OData queries:

```csharp
public static class WebApiConfig
{
    public static void Register(HttpConfiguration config)
    {
        // Wire-up OData and define the Entity Data Model
        config.UseOData(entityDataModelBuilder =>
        {
            entityDataModelBuilder.RegisterEntitySet<Category>("Categories", x => x.Name);
            entityDataModelBuilder.RegisterEntitySet<Employee>("Employees", x => x.EmailAddress);
            entityDataModelBuilder.RegisterEntitySet<Order>("Orders", x => x.OrderId);
            entityDataModelBuilder.RegisterEntitySet<Product>("Products", x => x.Name);
        });

        // Use Attribute Mapping for the OData controllers
        config.MapHttpAttributeRoutes();
    }
}
```

Note that when you register an Entity Set, you also specify the name of the Entity Set. The name needs to match the URL you intend to use so if you use `http://myservice/odata/Products` then register the Entity Set using `.RegisterEntitySet<Product>("Products", x => x.Name);`, if you use `http://myservice/odata/Product` then register the Entity Set using `.RegisterEntitySet<Product>("Product", x => x.Name);`.

## Usage

In your controller(s), define a Get method which accepts a single parameter of ODataQueryOptions:

```csharp
public IEnumerable<Category> Get(ODataQueryOptions queryOptions)
{
    // Implement query logic.
}
```

### Supported OData Versions

The library supports OData 4.0 query syntax, for a full list of supported features see [Supported OData Query](https://github.com/TrevorPilley/Net.Http.WebApi.OData/wiki/Supported-OData-Query) in the Wiki.

### Supported .NET Framework Versions

The NuGet Package contains binaries compiled against:

* .NET 4.5 and Microsoft.AspNet.WebApi.Core 5.2.7

To find out more, head over to the [Wiki](https://github.com/TrevorPilley/Net.Http.WebApi.OData/wiki).
