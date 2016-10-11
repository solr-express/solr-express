# ISearchParameterBuilder<_TDocument_>

When class implements this interface. Create facilities to create ISearchParameter 

## Any

Create a any parameter

> **NOTE**
> 
> Pay Attention! Need deactive **CheckAnyParameter** in DocumentCollectionOptions

* Param **_name_** Name of the parameter
* Param **_value_** Value of the parameter

## Boost

Create a boost parameter

* Param **_query_** Query used to make boost
* Param **_boostFunctionType_** Boost type used in calculation. Default is BoostFunctionType.Boost

## FacetField

Create a facet field parameter
* Param **_expression_** Expression used to find the property name
* Param **_sortType_** Sort type of the result of the facet
* Param **_limit_** Limit of itens in facet's result
* Param **_excludes_** List of tags to exclude in facet calculation

## FacetLimit

Create a facet limit parameter

* Param **_value_** Value of limit

## FacetQuery

Create a facet query parameter

* Param **_aliasName_** Name of the alias added in the query
* Param **_query_** Query used to make the facet
* Param **_sortType_** Sort type of the result of the facet
* Param **_excludes_** List of tags to exclude in facet calculation
IFacetQueryParameter<TDocument> FacetQuery(string aliasName, ISearchParameterValue query, FacetSortType? sortType = null, params string[] excludes);

## FacetRange

Create a facet range parameter

* Param **_aliasName_** Name of the alias added in the query
* Param **_expression_** Expression used to find the property name
* Param **_gap_** Size of each range bucket to make the facet
* Param **_start_** Lower bound to make the facet
* Param **_end_** Upper bound to make the facet
* Param **_sortType_** Sort type of the result of the facet

## Fields

Create a fields parameter
* Param **_expressions_** Expression used to find the property name
IFieldsParameter<TDocument> Fields(params Expression<Func<TDocument, object>>[] expressions);

## Filter (Overload#1)

Create a filter parameter

* Param **_expression_** Expression used to find the property name
* Param **_value_** Value of the filter
* Param **_tagName_** Tag name to use in facet excluding list

## Filter (Overload#2)

Create a filter parameter

* Param **_expression_** Expression used to find the property name
* Param **_from_** From value in a range filter
* Param **_to_** To value in a range filter
* Param **_tagName_** Tag name to use in facet excluding list

## Limit

Create a limit parameter

* Param **_value_** Value of limit

## MinimumShouldMatch

Create a minimum should match parameter

* Param **_expression_** Expression used to make the mm parameter

## Offset

Create a offset parameter

* Param **_value_** Value of limit

## Query (Overload#1)

Create a query parameter

* Param **_value_** Parameter to include in the query

## Query (Overload#2)

Create a query parameter

* Param **_value_** Parameter to include in the query

## Query (Overload#3)

Create a query parameter

* Param **_expression_** Expression used to find the property name
* Param **_value_** Value of the query

## QueryField

Create a query field parameter

* Param **_expression_** Expression used to make the mm parameter

## RandomSort

Create a random sort parameter

* Param **_ascendent_** True to ascendent order, otherwise false
* Param **_expressions_** Expression used to find the property name
IRandomSortParameter RandomSort(bool ascendent);

## Sort (Overload#1)

Create a sort parameter

* Param **_expression_** Expression used to find the property name
* Param **_ascendent_** True to ascendent order, otherwise false

## Sort (Overload#2)

Create a sort parameter

* Param **_ascendent_** True to ascendent order, otherwise false
* Param **_expressions_** Expression used to find the property name

## SpatialFilter

Create a query field parameter using spatial notation

* Param **_expression_** Expression used to find the property name
* Param **_functionType_** Function used in the spatial filter
* Param **_centerPoint_** Center point to spatial filter
* Param **_distance_** Distance from the center point