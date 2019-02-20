# Local parameter

## Feature

Create a local parameter

## How to

### Option 1

Using query (see how create queries in [tutorial](https://solr-express.gitbook.io/solr-express/tutorials/basic-features/queries))

```csharp
	DocumentCollection<TechProductDocument> documentCollection; // from DI

    var rersult = documentCollection
        .Select()
        .LocalParameter("MyLocalParameter", q => q.Categories, query => query.EqualsTo("some category"))
        .Execute();
```

### Option 2

Using plain value

```csharp
	DocumentCollection<TechProductDocument> documentCollection; // from DI

    var rersult = documentCollection
        .Select()
        .LocalParameter("MyLocalParameter", "some value")
        .Execute();
```
