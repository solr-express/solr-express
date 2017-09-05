# Facet field

## Feature

Create a facet field

## How to

1. Configure facet

```csharp
	DocumentCollection<TechProductDocument> documentCollection; // from DI

    var rersult = documentCollection
        .Select()
        .FacetField(q => q.InStock)
		.Execute();
```

2. Optionally, change one or more settings

```csharp
	DocumentCollection<TechProductDocument> documentCollection; // from DI

    var rersult = documentCollection
        .Select()
        .FacetField(q => q.InStock, facet =>
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
        // facetName = "InStock"
        var facetName = facetItem.Name;
        //facetType = FacetType.Field
        var facetType = facetItem.FacetType;

        foreach (var facetItemValue in facetItem.Values)
        {
            // nested = nested facet (if configured)
            var nested = facetItemValue.Facets;
            // key = value of item's value
            var key = facetItemValue.Key;
            // quantity = quantity of item's value
            var quantity = facetItemValue.Quantity;
        }
    }
```

## Settings

| Use case                                                                                       | How to                                                                                                    |
|------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------|
| Sort type of result of facet                                                                   | .FacetField(q => q.InStock, facet => facet.SortType(FacetSortType.CountAsc))                              |
| Minimum count of itens in facet's result                                                       | .FacetField(q => q.InStock, facet => facet.Minimum(2))                                                    |
| Limit of itens in facet's result                                                               | .FacetField(q => q.InStock, facet => facet.Limit(10))                                                     |
| List of tags to exclude in facet calculation                                                   | .FacetField(q => q.InStock, facet => facet.Excludes(new[] { "tag1", "tag2" }))                            |
| Specify a filter or list of filters to be intersected with the incoming domain before faceting | .FacetField(q => q.InStock, facet => facet => facet.Filter(f => f.Field(q => q.Popularity).EqualsTo(10))) |
| Method type to how to facet the field                                                          | .FacetField(q => q.InStock, facet => facet.MethodType(FacetSortType.UninvertedField))                     |
| Prefix to only produce buckets for terms starting with the specified value                     | .FacetField(q => q.Features, facet => facet.Prefix("some value"))                                         |

** NOTE **

Learn more about [queries](http://solr-express.readthedocs.io/en/stable/tutorials/basic-features/queries)
Learn more about [facets](http://yonik.com/json-facet-api/#Terms_Facet)