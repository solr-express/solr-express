# Facet field

## Feature

Create a facet range

## How to

1. Configure facet

```csharp
	DocumentCollection<TechProductDocument> documentCollection; // from DI

    var rersult = documentCollection
        .Select()
		// gap: "1", start: "10", end: "100"
        .FacetRange("AliasName", q => q.Price, "1", "10", "100")
		.Execute();
```

2. Optionally, change one or more settings

```csharp
	DocumentCollection<TechProductDocument> documentCollection; // from DI

    var rersult = documentCollection
        .Select()
        .FacetRange("AliasName", q => q.Price, "1", "10", "100", facet =>
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

    foreach (FacetItemRange facetItem in data)
    {
        // facetName = "AliasName"
        var facetName = facetItem.Name;
        //facetType = FacetType.Range
        var facetType = facetItem.FacetType;

        foreach (FacetItemRangeValue<decimal> facetItemValue in facetItem.Values)
        {
            // nested = nested facet (if configured)
            var nested = facetItemValue.Facets;
            // minimumValue = minimum value of item
            var minimumValue = facetItemValue.MinimumValue;
            // maximumValue = maximum value of item
            var maximumValue = facetItemValue.MaximumValue;
            // quantity = quantity of item's value
            var quantity = facetItemValue.Quantity;
        }
    }
```

## Settings

| Use case                                                                                                        | How to                                                                                                                |
|-----------------------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------------|
| Sort type of result of facet                                                                                    | .FacetRange("AliasName", q => q.Price, "1", "10", "100", facet => facet.SortType(FacetSortType.CountAsc))             |
| Minimum count of itens in facet's result                                                                        | .FacetRange("AliasName", q => q.Price, "1", "10", "100", facet => facet.Minimum(2))                                   |
| Limit of itens in facet's result                                                                                | .FacetRange("AliasName", q => q.Price, "1", "10", "100", facet => facet.Limit(10))                                    |
| List of tags to exclude in facet calculation                                                                    | .FacetRange("AliasName", q => q.Price, "1", "10", "100", facet => facet.Excludes(new[] { "tag1", "tag2" }))           |
| Counts should also be computed for all records with field values lower then lower bound of the first range      | .FacetRange("AliasName", q => q.Price, "1", "10", "100", facet => facet.CountBefore(true))                            |
| Counts should also be computed for all records with field values greater then the upper bound of the last range | .FacetRange("AliasName", q => q.Price, "1", "10", "100", facet => facet.CountAfter(true))                             |
| Specify a filter or list of filters to be intersected with the incoming domain before faceting                  | .FacetRange("AliasName", q => q.Price, "1", "10", "100", facet => facet.Filter(f => f.Field(q => q.Id).EqualsTo(10))) |
| Specify if last bucket will end at “end” even if it is less than “gap” wide                                     | .FacetRange("AliasName", q => q.Price, "1", "10", "100", facet => facet.HardEnd(true))                                |

** NOTE **

Learn more about [queries](http://solr-express.readthedocs.io/en/stable/tutorials/basic-features/queries)