**** IN CONSTRUCTION ****

# Solr Express

A simple and lightweight query .NET library for Solr

## Available at NuGet
If you want use [Solr 5.2 +](http://archive.apache.org/dist/lucene/solr/5.2.1)

```powershell
Install-Package SolrExpress.Solr5
```

If you want use [Solr 4.9 +](http://archive.apache.org/dist/lucene/solr/4.9.0)

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

### 2. Queries

### 3. Builders

### 4. Fluent API

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