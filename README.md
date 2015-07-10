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

TODO: Possibles parameters

Create a facet of field type using the informed field name and sort type

```csharp
myDocuments.Parameter(FacetFieldParameter<TestDocument>(q => q.Id));
//Optionally, set sort type
myDocuments.Parameter(FacetFieldParameter<TestDocument>(q => q.Id, SolrFacetSortType.CountDesc));
```

#### 1.2. FacetQueryParameter
#### 1.3. FacetRangeParameter
#### 1.4. FieldsParameter
#### 1.5. FilterParameter
#### 1.6. LimitParameter
#### 1.7. MinimumShouldMatchParameter
#### 1.8. OffsetParameter
#### 1.9. QueryFieldParameter
#### 1.10. QueryParameter
#### 1.11. SortParameter
#### 1.12. SpatialFilterParameter

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

TODO: How desactive
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