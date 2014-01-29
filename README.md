Net.Http.WebApi.OData
=====================

Net.Http.WebApi.OData is a C# library which parses an OData query uri into an object model which can be used to query custom data sources which are not IQueryable. It was extracted from the MicroLite.Extensions.WebApi library into a separate project so that it could be easily used by other projects.

To use it in your own Web API, firstly install the nuget package `Install-Package Net.Http.WebApi.OData` and then in your controller, define a Get method which accepts a single parameter of ODataQueryOptions:

    public IEnumerable<MyClass> Get(ODataQueryOptions queryOptions)
    {
        // Implement query logic.
    }

To find out more, head over to the [Wiki](https://github.com/TrevorPilley/Net.Http.WebApi.OData/wiki) and see how easy it is to use!