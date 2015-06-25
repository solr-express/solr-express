This is a draft doc version

# solr-express

A simple and lightweight query .NET library for Solr

## SolrExpress.Core
Main library with main logic

## SolrExpress.Solr4
Solr 4 query implementation

## SolrExpress.Solr5
Solr 5 query implementation

## Basic use
* Create a class and implement the IDocument interface

```csharp
    public class MyDocument : IDocument
    {
        [SolrField("id", Indexed = true, Stored = false, OmitNorms = false)]
        public string Id { get; set; }

        public decimal Score { get; set; }
    }
```

> You can add SolrFieldAttribute attribute to better property name control and to use Fail Fast feature

* Create a instance of SolrQueryable class (use a Factory or isolate this in a context). Set the Provider instance.

```csharp
    public class MyContext
    {
        public MyContext()
        {
            var myProvider = new Provider("http://localhost:8983/solr/mycollection");

            this.MyDocuments = new SolrQueryable<MyDocument>(myProvider);
        }

        public SolrQueryable<MyDocument> MyDocuments { get; private set; }
    }
```

* Use parameters

```csharp
var ctx = new MyContext();
// This will be create a query like http://localhost:8983/solr/mycollection/query?q=*:*
ctx.MyDocuments.Parameter(new QueryParameter(new QueryAll()));
```

* Execute the query

```csharp
var queryResult = ctx.MyDocuments.Execute();
```

* And get results

```csharp
var documents = queryResult.Get(new DocumentBuilder<MyDocument>()).Data;
```

Tan dam!! Done!
