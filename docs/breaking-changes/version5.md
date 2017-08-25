# Breaking changes in version 5.0

To full details of changes (why?, how? and for?), see [issue](https://github.com/solr-express/solr-express/issues/187)

## Supports

* No more support to **.Net 4.0** *(update to version 4.5)*
* No more support to **ISearchInterceptor** *(lost feature, incompatible with [issue](https://github.com/solr-express/solr-express/issues/171))*

## Renames

* Class **DocumentCollectionOptions** was renamed to **SolrExpressOptions** *(rename in your project)*
* Class **SearchResult** was renamed to **SearchResultBuilder** *(rename in your project)*
* Class **SimpleLogInConsoleResultInterceptor** was renamed to **SimpleLogInConsole** *(rename in your project)*
* Enumerator **SolrSpatialFunctionType** was renamed to **SpatialFunctionType** *(rename in your project)*
* Interface **IRandomSortParameter** was renamed to **ISortRandomlyParameter** *(rename in your project)*
* Interface **IResult** was renamed to **ISearchResult** (changing the mean of interface) *(rename in your project)*
* Interface **ISearchParameterCollection** was renamed to **ISearchItemCollection** *(rename in your project)*
* Interface **ISearchParameterExecute** was renamed to **ISearchItemExecution** *(rename in your project)*
* Method **Configure** from interface **IAtomicDelete** was renamed to **Execute** *(rename in your project)*
* Method **Configure** from interface **IAtomicUpdate** was renamed to **Execute** *(rename in your project)*
* Method **SetHandler** from class **DocumentSearch** was renamed to **Handler** *(rename in your project)*
* Namespace **SolrExpress.Core** was renamed to **SolrExpress** *(rename in your project)*
* Namespace from class **RequestHandler** was renamed to **SolrExpress.Search.Parameter** *(rename in your project)*
* Namespace from class **SolrSpatialFunctionType** was renamed to **SolrExpress.Core.Search** *(rename in your project)*
* Property **Expression** from interface **IFacetFieldParameter** was renamed to **FieldExpression** *(rename in your project)*
* Property **Expression** from interface **IFacetRangeParameter** was renamed to **FieldExpression** *(rename in your project)*
* Property **Expressions** from interface **IFieldsParameter** was renamed to **FieldExpressions** *(rename in your project)*

## Removes

### Follow interfaces was removed:

* IAtomicInstruction
* IConvertJsonObject
* IConvertJsonPlainText
* IDocument (use concret class **Document** instead) *(change in your project)*
* IDocumentCollection\<TDocument\> (use concret class **DocumentCollection\<TDocument\>** instead) *(change in your project)*
* IFacetFieldParameter (use concret class **FacetFieldParameter** instead) *(change in your project)*
* IFacetFieldResult (use interface **IFacetsResult** instead) *(change in your project)*
* IFacetQueryParameter (use concret class **FacetQueryParameter** instead) *(change in your project)*
* IFacetQueryResult (use interface **IFacetsResult** instead) *(change in your project)*
* IFacetRangeParameter (use concret class **FacetRangeParameter** instead) *(change in your project)*
* IFacetRangeResult (use interface **IFacetsResult** instead) *(change in your project)*
* IFacetSpatialParameter (use concret class **FacetSpatialParameter** instead) *(change in your project)*
* ISearchParameterValue (use concret class **SearchQuery** instead) *(change in your project)*
* ISolrAtomicUpdate (use concret class **DocumentUpdate** instead) *(change in your project)*
* ISolrSearch (use concret class **DocumentSearch** instead) *(change in your project)*
* IValidation

### Follow classes was removed:

* InformationBuilder
* InvalidParameterTypeException
* UnexpectedJsonFormatException
* UnknownResolveResultBuilderException

### Class **SolrFieldAttribute** lost properties:

* Indexed
* Stored
* OmitNorms

 > *[issue #112](https://github.com/solr-express/solr-express/issues/112)*

## Method Config

Follow interfaces lost method **Config** (Use extensions methods instead)

* IAnyParameter
* IBoostParameter
* IFacetFieldParameter
* IFacetLimitParameter
* IFacetQueryParameter
* IFacetRangeParameter
* IFacetSpatialParameter
* IFieldsParameter
* IFilterParameter
* ILimitParameter
* IMinimumShouldMatchParameter
* IOffsetParameter
* IQueryFieldParameter
* IQueryParameter
* IRandomSortParameter
* ISortParameter
* ISpatialFilterParameter