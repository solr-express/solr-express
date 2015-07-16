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

## Index

1. [Compatibility](https://github.com/solr-express/solr-express#compatibility)
2. [Packages](https://github.com/solr-express/solr-express#packages)
3. [Features](https://github.com/solr-express/solr-express#features)
	1. [Parameters](https://github.com/solr-express/solr-express#1-parameters)
	2. [Queries](https://github.com/solr-express/solr-express#2-queries)
	3. [Builders](https://github.com/solr-express/solr-express#3-builders)
	4. [Fluent API](https://github.com/solr-express/solr-express#4-fluent-api)
	5. [Friendly Field Name](https://github.com/solr-express/solr-express#5-friendly-field-name)
	6. [Fail Fast](https://github.com/solr-express/solr-express#6-fail-fast)
2. [Examples](https://github.com/solr-express/solr-express#examples)
	1. [Basic use](https://github.com/solr-express/solr-express#basic-use)
	2. [SearchUI](https://github.com/solr-express/solr-express#searchui)
3. [License](https://github.com/solr-express/solr-express#license)

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
new FacetFieldParameter<MyDocument>(q => q.Id, SolrFacetSortType.CountDesc);
```

#### 1.2. FacetQueryParameter

Create a facet query type parameter using the informed field alias, query class and sort type

```csharp
new FacetQueryParameter("Alias", new QueryAll(), SolrFacetSortType.CountDesc);
```

#### 1.3. FacetRangeParameter

Create a facet range type parameter using the informed field name, query class and sort type

```csharp
new FacetRangeParameter<MyDocument>("X", q => q.Price, "1", "10", "20", SolrFacetSortType.CountDesc);
```

#### 1.4. FieldsParameter

Create a fields parameter (field list in Solr 4) using the informed field list

* One by one

```csharp
new FieldListParameter<MyDocument>(q => q.Id);
new FieldListParameter<MyDocument>(q => q.Score);
```

* All in the same moment

```csharp
new FieldListParameter<MyDocument>(q => q.Id, q => q.Score);
```

#### 1.5. FilterParameter

Create a fields parameter (filter query in Solr 4) using the informed query class

```csharp
new FilterParameter(new SingleValue<MyDocument>(q => q.Id, "XPTO"));
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
new QueryParameter(new SingleValue<MyDocument>(q => q.Id, "XPTO"));
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
new SpatialFilterParameter<MyDocument>(SolrSpatialFunctionType.Geofilt, q => q.Spatial, new GeoCoordinate(-1.1M, -2.2M), 5.5M);

// Using Bbox function
new SpatialFilterParameter<MyDocument>(SolrSpatialFunctionType.Bbox, q => q.Spatial, new GeoCoordinate(-1.1M, -2.2M), 5.5M);
```

### 2. Queries

Allows create simple or complex queries in a controlled, buildable and testable way.

#### 2.1. QueryAll

Create a query to return all documents.

```csharp
// Create a query like "*:*"
new QueryAll();
```

#### 2.2. FreeValue

Create a free value query, this is weakest query class, because allows everything what you want.

Use very carefully.

```csharp
new FreeValue("Id:10");
```

#### 2.3. RangeValue

Create a range query.

```csharp
// Create a query like "Price:[1.5 TO 10.5]"
new RangeValue<MyDocument, decimal>(q => q.Price, 1.5M, 10.5M)
```

#### 2.4. SingleValue

Create a single value query, this is the easier way to create queries.

```csharp
// Create a query like City:"New York"
new SingleValue<MyDocument>(q => q.City, "New York");
```

#### 2.5. MultiValue

Create a container to complex queries using AND or OR operators.

```csharp
// Create a query like Price:[1.5 TO 10.5] AND City:"New York"
new MultiValue(SolrQueryConditionType.And, new RangeValue<MyDocument, decimal>(q => q.Price, 1.5M, 10.5M), new SingleValue<MyDocument>(q => q.City, "New York"));
```

```csharp
// Create a query like (Price:[1.5 TO 10.5] AND City:"New York") OR Id:"XPTO"
new MultiValue(SolrQueryConditionType.Or,
	new MultiValue(SolrQueryConditionType.And, new RangeValue<MyDocument, decimal>(q => q.Price, 1.5M, 10.5M), new SingleValue<MyDocument>(q => q.City, "New York")),
	new SingleValue<MyDocument>(q => q.Id, "XPTO"));
```

#### 2.6. NegativeValue

Create a container to negate the queries.

```csharp
// Create a query like -(Id:"Xpto")
new NegativeValue(new SingleValue<MyDocument>(q => q.Id, "XPTO"));
```

### 3. Builders

Allows parse Sorl result in a controlled and buildable way.

#### 3.1. DocumentBuilder

Parse the "documents" part of the Solr result in a list of MyDocument class.

```csharp
new DocumentBuilder<MyDocument>();
```

#### 3.2. FacetFieldResultBuilder

Parse the "facet.field" part of the Solr result in a list of FacetKeyValue class.

```csharp
new FacetFieldResultBuilder();
```

#### 3.3. FacetQueryResultBuilder

Parse the "facet.query" part of the Solr result in a instance of Dictionary<string, long>.

```csharp
new FacetQueryResultBuilder();
```

#### 3.4. FacetRangeResultBuilder

Parse the "facet.range" part of the Solr result in a instance of List<FacetKeyValue<FacetRange>>.

```csharp
new FacetQueryResultBuilder();
```

#### 3.5. StatisticResultBuilder

Parse the "statistic" part of the Solr result in a several properties with Solr statistics.

```csharp
new StatisticResultBuilder();
```

### 4. Fluent API
Allows use of fluent API to make the life easier and a beautiful code.

To do this, follow the steps:

* Use the namespace SolrExpress.Solr{Version}.Linq (where {version} is the Solr version of your choice)
* Use the extensions methods

To exemplify this, see the code below without and with the fluent api.

```csharp
	// Source without fluent API
	using (var ctx = new SolrContext())
	{
		List<TechProduct> documents;

		ctx.TechProducts
			.Parameter(new QueryParameter(new QueryAll()))
			.Parameter(new LimitParameter(3));

		var result = ctx.TechProducts.Execute();

		documents = result.Get(new DocumentBuilder<TechProduct>()).Data;
	}
```

```csharp
	// Source with fluent API
	using (var ctx = new SolrContext())
	{
		List<TechProduct> documents;

		ctx.TechProducts
			.Query(new QueryAll())
			.Limit(3);

		var result = ctx.TechProducts.Execute();

		result.Document<TechProduct>(out documents);
	}
```

### 5. Friendly field name
Allows use of SolrFieldAttribute attribute and control "from-to" field name between Solr document and POCO class.

```csharp
	public class MyDocument : IDocument
	{
		[SolrFieldAttribute("Field_With_A_Name_Hosted_In_Solr_Document")]
        public GeoCoordinate StoredAt { get; set; }
	}
```

### 6. Fail fast
Allows throws exceptions in some cases and make unit tests easier to be created.

To do this, use the SolrFieldAttribute attribute in properties of the POCO than represents the Solr document.

```csharp
	public class MyDocument : IDocument
	{
		[SolrFieldAttribute("StoredAt", Indexed = true, Stored = true, OmitNorms = true)]
        public GeoCoordinate StoredAt { get; set; }
	}
```

Each property of the attribute is validate in different moments. For example, indexed=false throws exception if the referenced property was used in FieldsParameter.

To all use cases, see [official wiki](http://wiki.apache.org/solr/FieldOptionsByUseCase)

To deactivate the fail fast feature (not recommended), when created the SolrQueryable, pass a configuration object in the constructor like the below code:

```csharp
	var provider = new Provider("http://localhost:8983/solr/techproducts");

	var config = new SolrQueryConfiguration
	{
		FailFast = false
	};

	this.TechProducts = new SolrQueryable<TechProduct>(provider, config);
```

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

All sorces of this example is available [here](https://github.com/solr-express/solr-express/tree/master/Examples/BasicUse)

### SearchUI

A fully implemented example is available [here](http://github.com/solr-express/solr-express/Examples/SearchUI)

## License

This software is licensed in [MIT License (MIT)](https://github.com/solr-express/solr-express/blob/master/LICENSE)