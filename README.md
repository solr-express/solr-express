# Solr Express
A simple and lightweight query .NET library for Solr, in a controlled, buildable and fail fast way.

## Available at NuGet
```powershell
Install-Package SolrExpress.Solr4
Install-Package SolrExpress.Solr5
```

> **Note:**
> - Solr 4.9 available [here](http://archive.apache.org/dist/lucene/solr/4.9.0)
> - Solr 5.5 available [here](http://archive.apache.org/dist/lucene/solr/5.5.0)

## Index
1. [Compatibility](#compatibility)
2. [Packages](#packages)
3. [Features](#features)
	1. [Parameters](#1-parameters)
	2. [Queries](#2-queries)
	3. [Results](#3-results)
	4. [Friendly Field Name](#4-friendly-field-name)
	5. [Fail Fast](#5-fail-fast)
2. [Examples](#examples)
	1. [Basic use](#basic-use)
	2. [SearchUI](#searchui)
3. [License](#license)

## Compatibility
| Framework | Compatibility |
|-----------|---------------|
| .Net 4.0  | Full          |
| .Net 4.5  | Full          |
| Core      | Full          |

| Solr | Compatibility                           | Tested                                       |
|------|-----------------------------------------|----------------------------------------------|
| 4.9  | Full                                    | Yes                                          |
| 5.5  | Full                                    | Yes                                          |
| 6.0  | Features created in Solr 5.x works well | Only features created in Solr 5.x works well |

## Packages
### SolrExpress.Core
Core library with main logic and generic implementations

### SolrExpress.Solr4
Solr 4 implementation, full compatibility with the [Solr 4.9+](http://archive.apache.org/dist/lucene/solr/4.9.0).

Implementation using default query handler and mechanism provided by request parameters.

### SolrExpress.Solr5
Solr 5 implementation, full compatibility with the [Solr 5.5+](http://archive.apache.org/dist/lucene/solr/5.5.0).

Implementation using default query handler and mechanism provided by JSON Request API.

## Features
### 1. Parameters
Allows send parameters to Sorl in a controlled and buildable way.

#### 1.1. FacetFieldParameter
Create a facet field type parameter using the informed field name and sort type.
```csharp
FacetField(q => q.Id)
```

#### 1.2. FacetQueryParameter
Create a facet query type parameter using the informed field alias, query class and sort type.
```csharp
FacetQuery("Alias", new QueryAll())
```

#### 1.3. FacetRangeParameter
Create a facet range type parameter using the informed field name, query class and sort type.
```csharp
FacetRange("X", q => q.Price, "1", "10", "20")
```

#### 1.4. FieldsParameter
Create a fields parameter (field list in Solr 4) using the informed field list.
```csharp
FieldList(q => q.Id)
FieldList(q => q.Score)
```
OR
```csharp
FieldListParameter(q => q.Id, q => q.Score)
```

#### 1.5. FilterParameter
Create a fields parameter (filter query in Solr 4) using the informed query class.
```csharp
Filter(q => q.Id, "XPTO")
```

#### 1.6. LimitParameter
Create a limit parameter (rows in Solr 4) using the informed number.
```csharp
Limit(50)
```

#### 1.7. MinimumShouldMatchParameter
Create a minimum should match parameter using the informed expression.
```csharp
MinimumShouldMatch("75%")
```

#### 1.8. OffsetParameter
Create a offset parameter (start in Solr 4) using the informed number.
```csharp
Offset(50)
```

#### 1.9. QueryFieldParameter
Create a query field parameter using the informed expression.
```csharp
QueryField("Id^10 Name^5~2")
```

#### 1.10. QueryParameter
Create a query parameter using the informed query class.
```csharp
Query(q => q.Id, "XPTO")
```

#### 1.11. SortParameter
Create a sort parameter using the informed expression and ascending type.
```csharp
Sort(q => q.Id)
```

#### 1.12. SpatialFilterParameter
Create a spatial filter parameter using the informed spatial function, expression, geo coordinate of origin and distance from origin point.
```csharp
// Using Geofilt function
SpatialFilter(q => q.Spatial, SolrSpatialFunctionType.Geofilt, new GeoCoordinate(-1.1M, -2.2M), 5.5M)

// Using Bbox function
SpatialFilter(q => q.Spatial, SolrSpatialFunctionType.Bbox, new GeoCoordinate(-1.1M, -2.2M), 5.5M)
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

### 3. Results

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


### 4. Friendly field name
Allows use of SolrFieldAttribute attribute and control "from-to" field name between Solr document and POCO class.

```csharp
	public class MyDocument : IDocument
	{
		[SolrField("Field_With_A_Name_Hosted_In_Solr_Document")]
        public GeoCoordinate StoredAt { get; set; }
	}
```

### 5. Fail fast
Allows throws exceptions in some cases and make unit tests easier to be created.

To do this, use the SolrFieldAttribute attribute in properties of the POCO than represents the Solr document.

```csharp
	public class MyDocument : IDocument
	{
		[SolrField("StoredAt", Indexed = true, Stored = true, OmitNorms = true)]
        public GeoCoordinate StoredAt { get; set; }
	}
```

Each property of the attribute is validate in different moments. For example, indexed=false throws exception if the referenced property was used in FieldsParameter.

To all use cases, see [official wiki](http://wiki.apache.org/solr/FieldOptionsByUseCase)

To deactivate fail fast feature (not recommended), when created the SolrQueryable, pass a configuration object in the constructor like the below code:

```csharp
    public class SolrContext : IDisposable
    {
        public SolrContext()
        {
            var provider = new Provider("http://localhost:8983/solr/techproducts");
            var resolver = new SimpleResolver().Configure();
            var configuration = new Configuration
            {
                FailFast = false // This deactivate fail fast feature
            };

            this.TechProducts = new DocumentCollection<TechProduct>(provider, resolver, configuration);
        }

        public DocumentCollection<TechProduct> TechProducts { get; private set; }

        public void Dispose()
        {
        }
    }
```

## Examples

### Basic use

Step by step to use the framework:

* Create a class and implement the IDocument interface

```csharp
    public class MyDocument : IDocument
```

* Create a instance of DocumentCollection class. Set the Provider, DI controller and Config instances.

```csharp
    var provider = new Provider("http://localhost:8983/solr/techproducts");
    var resolver = new SimpleResolver().Configure();
    var configuration = new Configuration();

    var myDocuments = new DocumentCollection<MyDocument>(provider, resolver, configuration);
```

* Use parameters

```csharp
// This will create a query like http://localhost:8983/solr/mycollection/query?q=*:*
var query = myDocuments.Select.Query(new QueryAll());
```

* Execute the query

```csharp
var queryResult = query.Execute();
```

* And get results

```csharp
List<MyDocument> documents;
queryResult.Document(out documents);
```

Tan dam!! Done!

All sorces of this example is available [here](https://github.com/solr-express/solr-express/blob/master/Sample.SimpleUse)

### SearchUI

A fully implemented example is available [here](https://github.com/solr-express/solr-express/blob/master/Sample.Ui)

## License

This software is licensed in [MIT License (MIT)](https://github.com/solr-express/solr-express/blob/master/LICENSE)