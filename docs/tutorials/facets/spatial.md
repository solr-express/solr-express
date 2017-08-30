# Facet field

## Feature

Create a facet spatial

## How to

1. Configure facet

```csharp
	DocumentCollection<TechProductDocument> documentCollection; // from DI

    var rersult = documentCollection
        .Select()
        .FacetSpatial("AliasName", q => q.StoredAt, new GeoCoordinate(1, 1), 10)
		.Execute();
```

2. Optionally, change one or more settings

```csharp
	DocumentCollection<TechProductDocument> documentCollection; // from DI

    var rersult = documentCollection
        .Select()
        .FacetSpatial("AliasName", q => q.StoredAt, new GeoCoordinate(1, 1), 10, facet =>
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

| Use case                                                                                       | How to                                                                                                                                |
|------------------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------------------------------------------------|
| Sort type of result of facet                                                                   | .FacetSpatial("AliasName", q => q.StoredAt, new GeoCoordinate(1, 1), 10, facet => facet.SortType(FacetSortType.CountAsc))             |
| Minimum count of itens in facet's result                                                       | .FacetSpatial("AliasName", q => q.StoredAt, new GeoCoordinate(1, 1), 10, facet => facet.Minimum(2))                                   |
| Limit of itens in facet's result                                                               | .FacetSpatial("AliasName", q => q.StoredAt, new GeoCoordinate(1, 1), 10, facet => facet.Limit(10))                                    |
| List of tags to exclude in facet calculation                                                   | .FacetSpatial("AliasName", q => q.StoredAt, new GeoCoordinate(1, 1), 10, facet => facet.Excludes(new[] { "tag1", "tag2" }))           |
| Function used in spatial filter                                                                | .FacetSpatial("AliasName", q => q.StoredAt, new GeoCoordinate(1, 1), 10, facet => facet.FunctionType(SpatialFunctionType.Bbox))       |
| Specify a filter or list of filters to be intersected with the incoming domain before faceting | .FacetSpatial("AliasName", q => q.StoredAt, new GeoCoordinate(1, 1), 10, facet => facet.Filter(f => f.Field(q => q.Id).EqualsTo(10))) |

** NOTE **

Learn more about [queries](http://solr-express.readthedocs.io/en/stable/tutorials/basic-features/queries)