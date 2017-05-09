Net.Http.WebApi.OData
=====================

[![NuGet version](https://badge.fury.io/nu/Net.Http.WebApi.OData.svg)](http://badge.fury.io/nu/Net.Http.WebApi.OData)

Net.Http.WebApi.OData is a C# library which parses an OData query uri into an object model which can be used to query custom data sources which are not IQueryable. It was extracted from the [MicroLite.Extensions.WebApi](https://github.com/TrevorPilley/MicroLite.Extensions.WebApi) library into a separate project so that it could be easily used by others.

## Installation

To use it in your own Web API you need to install the nuget package `Install-Package Net.Http.WebApi.OData -Version 4.0.0`

## Configuration

Somewhere in your application startup/webapi configuration, specify the types which will be used for OData queries:

```csharp
public static class WebApiConfig
{
    public static void Register(HttpConfiguration config)
    {
        // Build the OData Entity Data Model first
        var entityDataModelBuilder = new EntityDataModelBuilder();
        entityDataModelBuilder.RegisterEntitySet<Category>("Categories");
        entityDataModelBuilder.RegisterEntitySet<Product>("Products");
        entityDataModelBuilder.BuildModel();

        // Configure routes, etc
    }
}
```

Note that when you register an Entity Set, you also specify the name of the Entity Set. The name needs to match the URL you intend to use so if you use `http://myservice/api/Products` then register the Entity Set using `.RegisterEntitySet<Product>("Products");`, if you use `http://myservice/api/Product` then register the Entity Set using `.RegisterEntitySet<Product>("Product");`.

## Usage

In your controller(s), define a Get method which accepts a single parameter of ODataQueryOptions:

```csharp
public IEnumerable<Category> Get(ODataQueryOptions queryOptions)
{
    // Implement query logic.
}
```

### Supported OData Versions

The library supports OData 4.0

### Supported .NET Framework Versions

The NuGet Package contains binaries compiled against:

* .NET 4.5 and Microsoft.AspNet.WebApi.Core 5.2.3

To find out more, head over to the [Wiki](https://github.com/TrevorPilley/Net.Http.WebApi.OData/wiki).