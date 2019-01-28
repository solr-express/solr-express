# Breaking changes in version 5.0

To full details of changes (why?, how? and for?), see [issue](https://github.com/solr-express/solr-express/issues/187)

## Supports

-   No more support to **.Net 4.0** _(update to version 4.5)_
-   No more support to **ISearchInterceptor** _(lost feature, incompatible with [issue](https://github.com/solr-express/solr-express/issues/171))_

## Renames

-   Class **DocumentCollectionOptions** was renamed to **SolrExpressOptions** _(rename in your project)_
-   Class **SearchResult** was renamed to **SearchResultBuilder** _(rename in your project)_
-   Class **SimpleLogInConsoleResultInterceptor** was renamed to **SimpleLogInConsole** _(rename in your project)_
-   Enumerator **SolrSpatialFunctionType** was renamed to **SpatialFunctionType** _(rename in your project)_
-   Interface **IRandomSortParameter** was renamed to **ISortRandomlyParameter** _(rename in your project)_
-   Interface **IResult** was renamed to **ISearchResult** (changing the mean of interface) _(rename in your project)_
-   Interface **ISearchParameterCollection** was renamed to **ISearchItemCollection** _(rename in your project)_
-   Interface **ISearchParameterExecute** was renamed to **ISearchItemExecution** _(rename in your project)_
-   Method **Configure** from interface **IAtomicDelete** was renamed to **Execute** _(rename in your project)_
-   Method **Configure** from interface **IAtomicUpdate** was renamed to **Execute** _(rename in your project)_
-   Method **SetHandler** from class **DocumentSearch** was renamed to **Handler** _(rename in your project)_
-   Namespace **SolrExpress.Core** was renamed to **SolrExpress** _(rename in your project)_
-   Namespace from class **RequestHandler** was renamed to **SolrExpress.Search.Parameter** _(rename in your project)_
-   Namespace from class **SolrSpatialFunctionType** was renamed to **SolrExpress.Core.Search** _(rename in your project)_
-   Property **Expression** from interface **IFacetFieldParameter** was renamed to **FieldExpression** _(rename in your project)_
-   Property **Expression** from interface **IFacetRangeParameter** was renamed to **FieldExpression** _(rename in your project)_
-   Property **Expressions** from interface **IFieldsParameter** was renamed to **FieldExpressions** _(rename in your project)_

## Removes

### Follow interfaces was removed:

-   IAtomicInstruction
-   IConvertJsonObject
-   IConvertJsonPlainText
-   IDocument (use concret class **Document** instead) _(change in your project)_
-   IDocumentCollection\\&lt;TDocument> (use concret class **DocumentCollection\\&lt;TDocument>** instead) _(change in your project)_
-   IFacetFieldParameter (use concret class **FacetFieldParameter** instead) _(change in your project)_
-   IFacetFieldResult (use interface **IFacetsResult** instead) _(change in your project)_
-   IFacetQueryParameter (use concret class **FacetQueryParameter** instead) _(change in your project)_
-   IFacetQueryResult (use interface **IFacetsResult** instead) _(change in your project)_
-   IFacetRangeParameter (use concret class **FacetRangeParameter** instead) _(change in your project)_
-   IFacetRangeResult (use interface **IFacetsResult** instead) _(change in your project)_
-   IFacetSpatialParameter (use concret class **FacetSpatialParameter** instead) _(change in your project)_
-   ISearchParameterValue (use concret class **SearchQuery** instead) _(change in your project)_
-   ISolrAtomicUpdate (use concret class **DocumentUpdate** instead) _(change in your project)_
-   ISolrSearch (use concret class **DocumentSearch** instead) _(change in your project)_
-   IValidation

### Follow classes was removed:

-   InformationBuilder
-   InvalidParameterTypeException
-   UnexpectedJsonFormatException
-   UnknownResolveResultBuilderException

### Class **SolrFieldAttribute** lost properties:

-   Indexed
-   Stored
-   OmitNorms

    > _[issue #112](https://github.com/solr-express/solr-express/issues/112)_

## Method Config

Follow interfaces lost method **Config** (Use extensions methods instead)

-   IAnyParameter
-   IBoostParameter
-   IFacetFieldParameter
-   IFacetLimitParameter
-   IFacetQueryParameter
-   IFacetRangeParameter
-   IFacetSpatialParameter
-   IFieldsParameter
-   IFilterParameter
-   ILimitParameter
-   IMinimumShouldMatchParameter
-   IOffsetParameter
-   IQueryFieldParameter
-   IQueryParameter
-   IRandomSortParameter
-   ISortParameter
-   ISpatialFilterParameter
