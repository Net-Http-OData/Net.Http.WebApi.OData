Net.Http.WebApi.OData
=====================

|Service|Status|
|-------|------|
||[![NuGet version](https://badge.fury.io/nu/Net.Http.WebApi.OData.svg)](http://badge.fury.io/nu/Net.Http.WebApi.OData)|
|/develop|[![Build Status](https://dev.azure.com/trevorpilley/Net.Http.OData/_apis/build/status/Net-Http-OData.Net.Http.WebApi.OData?branchName=develop)](https://dev.azure.com/trevorpilley/Net.Http.OData/_build/latest?definitionId=20&branchName=develop)|
|/master|[![Build Status](https://dev.azure.com/trevorpilley/Net.Http.OData/_apis/build/status/Net-Http-OData.Net.Http.WebApi.OData?branchName=master)](https://dev.azure.com/trevorpilley/Net.Http.OData/_build/latest?definitionId=20&branchName=master)|

Net.Http.WebApi.OData is a .NET 4.5 library which uses [Net.Http.OData](https://github.com/Net-Http-OData/Net.Http.OData) with an implementation for ASP.NET WebApi.

## Installation

Install the nuget package `Install-Package Net.Http.WebApi.OData`

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
            entityDataModelBuilder.RegisterEntitySet<Category>("Categories", x => x.Name)
                .RegisterEntitySet<Employee>("Employees", x => x.EmailAddress)
                .RegisterEntitySet<Order>("Orders", x => x.OrderId)
                .RegisterEntitySet<Product>("Products", x => x.Name);
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
public class MyController : ODataController
{
    public IHttpActionResult Get(ODataQueryOptions queryOptions)
    {
        // Implement query logic.
        var results = ...

        var responseContent = new ODataResponseContent { Value = results };

        if (queryOptions.Count)
        {
            responseContent.Count = results.TotalCount;
        }

        return Ok(responseContent);
    }
}
```

### Supported OData Versions

The library supports OData 4.0 query syntax, for a full list of supported features see [Supported Query Syntax](https://github.com/Net-Http-OData/Net.Http.OData/wiki/Supported-Query-Syntax) in the `Net.Http.OData` Wiki.

### Supported .NET Versions

The NuGet Package contains binaries compiled against:

* .NET Framework 4.5
  * Microsoft.AspNet.WebApi.Core 5.2.7
  * Net.Http.OData 5.0.0

To find out more, head over to the [Wiki](https://github.com/Net-Http-OData/Net.Http.WebApi.OData/wiki).
