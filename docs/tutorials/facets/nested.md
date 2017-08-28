# Facet field

## Feature

Create a nested facet

## How to

1. Configure facet (field, range, query or spatial)

```csharp
	DocumentCollection<TechProductDocument> documentCollection; // from DI

    var rersult = documentCollection
        .Select()
        .FacetField(q => q.InStock)
		.Execute();
```

2. Configure nested facet (field, range, query or spatial)

```csharp
	DocumentCollection<TechProductDocument> documentCollection; // from DI

    var rersult = documentCollection
        .Select()
        .FacetField(q => q.InStock, facet => facet
            .FacetField(q => q.Categories))
        .Execute()
        .Facets(out var data);
```

3. Follow instructions to read data of a facet [field](http://solr-express.readthedocs.io/en/stable/tutorials/facets/field), [range](http://solr-express.readthedocs.io/en/stable/tutorials/facets/range), [query](http://solr-express.readthedocs.io/en/stable/tutorials/facets/query) or [spatial](http://solr-express.readthedocs.io/en/stable/tutorials/facets/spatial)