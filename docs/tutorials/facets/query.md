# Facet field

## Feature

Create a facet query

## How to

1. Configure facet

```csharp
	DocumentCollection<TechProductDocument> documentCollection; // from DI

    var rersult = documentCollection
        .Select()
        .FacetQuery("AliasName", q => q.Field(f => f.Features))
		.Execute();
```

2. Optionally, change one or more settings

```csharp
	DocumentCollection<TechProductDocument> documentCollection; // from DI

    var rersult = documentCollection
        .Select()
        .FacetQuery("AliasName", q => q.Field(f => f.Features), facet =>
        {
            facet.Minimum = 3;
            facet.Limit = 5;
        })
		.Execute();
```

3. Read data

```csharp
	rersult
		.Facets(out var data);

    foreach (FacetItemField facetItem in data)
    {
        // facetName = "InStock" or some alias (if configured)
        var facetName = facetItem.Name;
        //facetType = FacetType.Field
        var facetType = facetItem.FacetType;

        foreach (FacetItemQuery facetItem in data)
        {
            // facetName = "AliasName"
            var facetName = facetItem.Name;
            //facetType = FacetType.Query
            var facetType = facetItem.FacetType;
            // nested = nested facet (if configured)
            var nested = facetItem.Facets;
            // quantity = quantity of item's value
            var quantity = facetItem.Quantity;
        }
    }
```

## Settings

| Use case                                                                                       | How to                                                                                                               |
|------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------|
| Sort type of result of facet                                                                   | .FacetQuery("AliasName", q => q.Field(f => f.Features), facet => facet.SortType(FacetSortType.CountAsc))             |
| Minimum count of itens in facet's result                                                       | .FacetQuery("AliasName", q => q.Field(f => f.Features), facet => facet.Minimum(2))                                   |
| Limit of itens in facet's result                                                               | .FacetQuery("AliasName", q => q.Field(f => f.Features), facet => facet.Limit(10))                                    |
| List of tags to exclude in facet calculation                                                   | .FacetQuery("AliasName", q => q.Field(f => f.Features), facet => facet.Excludes(new[] { "tag1", "tag2" }))           |
| Specify a filter or list of filters to be intersected with the incoming domain before faceting | .FacetQuery("AliasName", q => q.Field(f => f.Features), facet => facet.Filter(f => f.Field(q => q.Id).EqualsTo(10))) |

** NOTE **

Learn more about [queries](http://solr-express.readthedocs.io/en/stable/tutorials/basic-features/queries)