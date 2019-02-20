# Nested facet

## Feature

Create a nested facet

## How to

1.  Configure facet (field, range, query or spatial)

```csharp
	DocumentCollection<TechProductDocument> documentCollection; // from DI

    var rersult = documentCollection
        .Select()
        .FacetField(q => q.InStock)
		.Execute();
```

2.  Configure nested facet (field, range, query or spatial)

```csharp
	DocumentCollection<TechProductDocument> documentCollection; // from DI

    var rersult = documentCollection
        .Select()
        .FacetField(q => q.InStock, facet => facet
            .FacetField(q => q.Categories))
        .Execute()
        .Facets(out var data);
```

3.  Follow instructions to read data of a facet [field](https://solr-express.gitbook.io/solr-express/tutorials/facets/field), [range](https://solr-express.gitbook.io/solr-express/tutorials/facets/range), [query](https://solr-express.gitbook.io/solr-express/tutorials/facets/query) or [spatial](https://solr-express.gitbook.io/solr-express/tutorials/facets/spatial)
