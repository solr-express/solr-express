**** IN CONSTRUCTION ****

# Solr Express

A simple and lightweight query .NET library for Solr, in a controlled, buildable and fail fast way.

## Available at NuGet
If you want to use [Solr 5.2 +](http://archive.apache.org/dist/lucene/solr/5.2.1)

```powershell
Install-Package SolrExpress.Solr5
```

If you want to use [Solr 4.9 +](http://archive.apache.org/dist/lucene/solr/4.9.0)

```powershell
Install-Package SolrExpress.Solr4
```

## Compatibility

.Net Framework 4.5 or higher

## Packages

### SolrExpress.Core
Core library with main logic and generic implementations

### SolrExpress.Solr4
Solr 4 implementation, full compatibility with the [Solr 4.9 +](http://archive.apache.org/dist/lucene/solr/4.9.0) version.

Implementation using default query handler and mechanism provided by request parameters.

### SolrExpress.Solr5
Solr 5 implementation, full compatibility with the [Solr 5.2 +](http://archive.apache.org/dist/lucene/solr/5.2.1) version.

Implementation using default query handler and mechanism provided by JSON Request API.

## Features

### 1. Parameters
Allows send parameters to Sorl in a controlled and buildable way.

#### 1.1. FacetFieldParameter

Create a facet field type parameter using the informed field name and sort type

```csharp
new FacetFieldParameter<TestDocument>(q => q.Id, SolrFacetSortType.CountDesc);
```

#### 1.2. FacetQueryParameter

Create a facet query type parameter using the informed field alias, query class and sort type

```csharp
new FacetQueryParameter("Alias", new QueryAll(), SolrFacetSortType.CountDesc);
```

#### 1.3. FacetRangeParameter

Create a facet range type parameter using the informed field name, query class and sort type

```csharp
new FacetRangeParameter<TestDocument>("X", q => q.Price, "1", "10", "20", SolrFacetSortType.CountDesc);
```

#### 1.4. FieldsParameter

Create a fields parameter (field list in Solr 4) using the informed field list

* One by one

```csharp
new FieldListParameter<TestDocument>(q => q.Id);
new FieldListParameter<TestDocument>(q => q.Score);
```

* All in the same moment

```csharp
new FieldListParameter<TestDocument>(q => q.Id, q => q.Score);
```

#### 1.5. FilterParameter

Create a fields parameter (filter query in Solr 4) using the informed query class

```csharp
new FilterParameter(new SingleValue<TestDocument>(q => q.Id, "XPTO"));
```

#### 1.6. LimitParameter

Create a limit parameter (rows in Solr 4) using the informed number

```csharp
new LimitParameter(50);
```

#### 1.7. MinimumShouldMatchParameter

Create a minimum should match parameter using the informed expression

```csharp
new MinimumShouldMatchParameter("75%");
```

#### 1.8. OffsetParameter

Create a offset parameter (start in Solr 4) using the informed number

```csharp
new OffsetParameter(50);
```

#### 1.9. QueryFieldParameter

Create a query field parameter using the informed expression

```csharp
new QueryFieldParameter("Id^10 Name^5~2");
```

#### 1.10. QueryParameter

Create a query parameter using the informed query class

```csharp
new QueryParameter(new SingleValue<TestDocument>(q => q.Id, "XPTO"));
```

#### 1.11. SortParameter

Create a sort parameter using the informed expression and ascending type

```csharp
new SortParameter(q => q.Id, true);
```

#### 1.12. SpatialFilterParameter

Create a spatial filter parameter using the informed spatial function, expression, geo coordinate of origin and distance from origin point

```csharp
// Using Geofilt function
new SpatialFilterParameter<TestDocument>(SolrSpatialFunctionType.Geofilt, q => q.Spatial, new GeoCoordinate(-1.1M, -2.2M), 5.5M);

// Using Bbox function
new SpatialFilterParameter<TestDocument>(SolrSpatialFunctionType.Bbox, q => q.Spatial, new GeoCoordinate(-1.1M, -2.2M), 5.5M);
```

### 2. Queries

TODO: Comment about

#### 2.1. FreeValue
#### 2.2. MultiValue
#### 2.3. NegativeValue
#### 2.4. QueryAll
#### 2.5. RangeValue
#### 2.6. SingleValue

### 3. Builders

TODO: Comment about

#### 3.l. DocumentBuilder
#### 3.2. FacetFieldResultBuilder
#### 3.3. FacetQueryResultBuilder
#### 3.4. FacetRangeResultBuilder
#### 3.5. StatisticResultBuilder

### 4. Fluent API
Allows use of fluent API to make the life easier and a beautiful code.

TODO: Source without fluent API

TODO: Source with fluent API

### 5. Friendly field name
Allows use of SolrFieldAttribute attribute and control "from-to" field name between Solr document and POCO class.

TODO: Example

### 6. Fail fast
Allows throws exceptions in some cases and make unit tests easier to be created.

TODO: How inactive
TODO: Example

## Examples

### Basic use

Step to step to use the framework:

* Create a class and implement the IDocument interface

```csharp
    public class MyDocument : IDocument
```

* Create a instance of SolrQueryable class. Set the Provider instance.

```csharp
    var myProvider = new Provider("http://localhost:8983/solr/mycollection");

    var myDocuments = new SolrQueryable<MyDocument>(myProvider);
```

* Use parameters

```csharp
// This will create a query like http://localhost:8983/solr/mycollection/query?q=*:*
myDocuments.Parameter(new QueryParameter(new QueryAll()));
```

* Execute the query

```csharp
var queryResult = myDocuments.Execute();
```

* And get results

```csharp
var documents = queryResult.Get(new DocumentBuilder<MyDocument>()).Data;
```

Tan dam!! Done!

All sorces of this example is available [here](http://github.com/solr-express/solr-express/Examples/BaseUse)

### Full example

A fully implemented example is available [here](http://github.com/solr-express/solr-express/Examples/SearchUI)

## License

This software is licensed in [MIT License (MIT)](https://github.com/solr-express/solr-express/blob/master/LICENSE)