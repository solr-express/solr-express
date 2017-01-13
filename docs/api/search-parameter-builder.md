# ISearchParameterBuilder<_TDocument_>

When class implements this interface. Create facilities to create ISearchParameter 

## Any

Create a any parameter

> **NOTE**
> 
> Pay Attention! Need deactive **CheckAnyParameter** in DocumentCollectionOptions

* Param **_name_** Name of the parameter
* Param **_value_** Value of the parameter

```
public static ISolrSearch<TDocument> Any(this ISolrSearch<TDocument> search, string name, string value)
	where TDocument : IDocument
``` 

## Boost

Create a boost parameter

* Param **_query_** Query used to make boost
* Param **_boostFunctionType_** Boost type used in calculation. Default is BoostFunctionType.Boost

```
public static ISolrSearch<TDocument> Boost<TDocument>(this ISolrSearch<TDocument> search,
    ISearchParameterValue<TDocument> query, BoostFunctionType? boostFunctionType = null)
	where TDocument : IDocument
```

## FacetField

Create a facet field parameter
* Param **_expression_** Expression used to find the property name
* Param **_sortType_** Sort type of the result of the facet
* Param **_limit_** Limit of itens in facet's result
* Param **_excludes_** List of tags to exclude in facet calculation

```
public static ISolrSearch<TDocument> FacetField<TDocument>(this ISolrSearch<TDocument> search,
    Expression<Func<TDocument, object>> expression, FacetSortType? sortType = null,
    int? limit = null, params string[] excludes)
	where TDocument : IDocument
```

## FacetLimit

Create a facet limit parameter

* Param **_value_** Value of limit
```
public static ISolrSearch<TDocument> FacetLimit<TDocument>(this ISolrSearch<TDocument> search, int value)
	where TDocument : IDocument
```

## FacetQuery

Create a facet query parameter

* Param **_aliasName_** Name of the alias added in the query
* Param **_query_** Query used to make the facet
* Param **_sortType_** Sort type of the result of the facet
* Param **_excludes_** List of tags to exclude in facet calculation

```
public static ISolrSearch<TDocument> FacetQuery<TDocument>(this ISolrSearch<TDocument> search,
    string aliasName, ISearchParameterValue<TDocument> query, FacetSortType? sortType = null,
    params string[] excludes)
	where TDocument : IDocument
```

## FacetRange

Create a facet range parameter

* Param **_aliasName_** Name of the alias added in the query
* Param **_expression_** Expression used to find the property name
* Param **_gap_** Size of each range bucket to make the facet
* Param **_start_** Lower bound to make the facet
* Param **_end_** Upper bound to make the facet
* Param **_sortType_** Sort type of the result of the facet
* Param **_countBefore_** Counts should also be computed for all records with field values lower then lower bound of the first range
* Param **_countAfter_** Counts should also be computed for all records with field values greater then the upper bound of the last range
* Param **_sortType_** Sort type of the result of the facet
* Param **_excludes_** List of tags to exclude in facet calculation

```
public static ISolrSearch<TDocument> FacetRange<TDocument>(this ISolrSearch<TDocument> search, string aliasName,
    Expression<Func<TDocument, object>> expression, string gap = null, string start = null, string end = null,
    bool countBefore = false, bool countAfter = false, FacetSortType? sortType = null, params string[] excludes)
	where TDocument : IDocument
```

## Fields

Create a fields parameter
* Param **_expressions_** Expression used to find the property name
IFieldsParameter<TDocument> Fields(params Expression<Func<TDocument, object>>[] expressions);

```
public static ISolrSearch<TDocument> Fields<TDocument>(this ISolrSearch<TDocument> search,
    params Expression<Func<TDocument, object>>[] expressions)
	where TDocument : IDocument
```

## Filter (Overload#1)

Create a filter parameter

* Param **_expression_** Expression used to find the property name
* Param **_value_** Value of the filter
* Param **_tagName_** Tag name to use in facet excluding list

```
public static ISolrSearch<TDocument> Filter<TDocument>(this ISolrSearch<TDocument> search,
    Expression<Func<TDocument, object>> expression, string value, string tagName = null)
	where TDocument : IDocument
```

## Filter (Overload#2)

Create a filter parameter

* Param **_expression_** Expression used to find the property name
* Param **_from_** From value in a range filter
* Param **_to_** To value in a range filter
* Param **_tagName_** Tag name to use in facet excluding list

```
public static ISolrSearch<TDocument> Filter<TDocument, TValue>(this ISolrSearch<TDocument> search,
    Expression<Func<TDocument, object>> expression, TValue? from, TValue? to, string tagName = null)
	where TDocument : IDocument
	where TValue : struct
```

## Filter (Overload#3)

Create a filter parameter

* Param **_value_** Value of the filter

```
public static ISolrSearch<TDocument> Filter<TDocument>(this ISolrSearch<TDocument> search, ISearchParameterValue<TDocument> value)
	where TDocument : IDocument
```

## Limit

Create a limit parameter

* Param **_value_** Value of limit

```
public static ISolrSearch<TDocument> Limit<TDocument>(this ISolrSearch<TDocument> search, int value)
	where TDocument : IDocument
```

## MinimumShouldMatch

Create a minimum should match parameter

* Param **_expression_** Expression used to make the mm parameter

```
public static ISolrSearch<TDocument> MinimumShouldMatch<TDocument>(this ISolrSearch<TDocument> search, string expression)
	where TDocument : IDocument
```

## Offset

Create a offset parameter

* Param **_value_** Value of limit

```
public static ISolrSearch<TDocument> Offset<TDocument>(this ISolrSearch<TDocument> search, int value)
	where TDocument : IDocument
```

## QueryAll

Create a query parameter with "*:*"

```
public static ISolrSearch<TDocument> QueryAll<TDocument>(this ISolrSearch<TDocument> search)
	where TDocument : IDocument
```

## Query (Overload#1)

Create a query parameter

* Param **_value_** Parameter to include in the query

```
public static ISolrSearch<TDocument> Query<TDocument>(this ISolrSearch<TDocument> search, ISearchParameterValue<TDocument> value)
	where TDocument : IDocument
```

## Query (Overload#2)

Create a query parameter

* Param **_value_** Parameter to include in the query

```
public static ISolrSearch<TDocument> Query<TDocument>(this ISolrSearch<TDocument> search, string value)
	where TDocument : IDocument
```

## Query (Overload#3)

Create a query parameter

* Param **_expression_** Expression used to find the property name
* Param **_value_** Value of the query

```
public static ISolrSearch<TDocument> Query<TDocument>(this ISolrSearch<TDocument> search,
    Expression<Func<TDocument, object>> expression, string value)
	where TDocument : IDocument
```

## QueryField

Create a query field parameter

* Param **_expression_** Expression used to make the mm parameter

```
public static ISolrSearch<TDocument> QueryField<TDocument>(this ISolrSearch<TDocument> search, string expression)
	where TDocument : IDocument
```

## RandomSort

Create a random sort parameter

* Param **_ascendent_** True to ascendent order, otherwise false
* Param **_expressions_** Expression used to find the property name

```
public static ISolrSearch<TDocument> RandomSort<TDocument>(this ISolrSearch<TDocument> search, bool ascendent)
	where TDocument : IDocument
```

## Sort (Overload#1)

Create a sort parameter

* Param **_expression_** Expression used to find the property name
* Param **_ascendent_** True to ascendent order, otherwise false

```
public static ISolrSearch<TDocument> Sort<TDocument>(this ISolrSearch<TDocument> search,
    Expression<Func<TDocument, object>> expression, bool ascendent)
	where TDocument : IDocument
```

## Sort (Overload#2)

Create a sort parameter

* Param **_ascendent_** True to ascendent order, otherwise false
* Param **_expressions_** Expression used to find the property name

```
public static ISolrSearch<TDocument> Sort<TDocument>(this ISolrSearch<TDocument> search, bool ascendent,
    params Expression<Func<TDocument, object>>[] expressions)
	where TDocument : IDocument
```

## SpatialFilter

Create a query field parameter using spatial notation

* Param **_expression_** Expression used to find the property name
* Param **_functionType_** Function used in the spatial filter
* Param **_centerPoint_** Center point to spatial filter
* Param **_distance_** Distance from the center point

```
public static ISolrSearch<TDocument> SpatialFilter<TDocument>(this ISolrSearch<TDocument> search,
    Expression<Func<TDocument, object>> expression, SolrSpatialFunctionType functionType,
    GeoCoordinate centerPoint, decimal distance)
	where TDocument : IDocument
```

## Page

Create a offset/limit parameters

* Param **_limit_** Value of limit
* Param **_page_** Value of page

```
public static ISolrSearch<TDocument> Page<TDocument>(this ISolrSearch<TDocument> search, int limit, int page)
	where TDocument : IDocument
```