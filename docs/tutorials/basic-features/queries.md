# Queries

## Feature

Create a query to filter or to use in filter query

## How to

1.  Use some feature that allow use **SearchQuery&lt;>** (i.e. _Filter_ or _FacetQuery_)

2.  Configure query

```csharp
	DocumentCollection<TechProductDocument> documentCollection; // from DI

    var rersult = documentCollection
        .Select()
		 // cat:"some category"
        .Filter(q => q.Categories, query => query.EqualsTo("some category"))
        .Execute();
```

3.  Optionally use a chain of methods

```csharp
	DocumentCollection<TechProductDocument> documentCollection; // from DI

    var rersult = documentCollection
        .Select()
		 // cat:"some category" OR features:("feature1" OR "feature2")
        .Filter(q => q.Categories, query => query
            .EqualsTo("some category")
            .Or(nested => nested
                .Field(f => f.Features)
                .Any("feature1", "feature2")))
        .Execute();
```

## Simple cases

| Use case                                               | How to                                                       | Query generated                   |
| ------------------------------------------------------ | ------------------------------------------------------------ | --------------------------------- |
| Query to find all informed values (conditional AND)    | query.Field(f => f.Categories).All("category1", "category2") | cat:("category1" AND "category2") |
| Query to find some of informed values (conditional OR) | query.Field(f => f.Categories).Any("category1", "category2") | cat:("category1" OR "category2")  |
| Query to find something starts with informed value     | query.Field(f => f.Categories).StartsWith("c")               | cat:"c\*"                         |
| Query to find exact informed value                     | query.Field(f => f.Categories).EqualsTo("category1")         | cat:"category1"                   |
| Query to find negate informed value                    | query.Field(f => f.Categories).NotEqualsTo("category1")      | NOT(cat:"category1")              |
| Query to find someting in informed range               | query.Field(f => f.Price).InRange(1, 10)                     | price:[1 TO 10]                   |
| Query to find someting greater than informed value     | query.Field(f => f.Price).GreaterThan(1)                     | price:[1 TO *]                    |
| Query to find someting less than informed value        | query.Field(f => f.Price).LessThan(1)                        | price:[* TO 10]                   |

# Complex queries

| Use case                              | How to                                                                                                                             | Query generated                           |
| ------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- | ----------------------------------------- |
| Query expression isolating in a group | query.Group(price=> price.Filed(f => f.Price).InRange(1, 10).Or(popularity => popularity.Field(f => f.Popularity).GreaterThan(5))) | (price:[1 TO 10] OR  popularity:[5 TO *]) |
