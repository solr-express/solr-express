# Migration from version 4.x to version 5.x

---

DRAFT

---

- Namespace **SolrExpress.Core** to **SolrExpress**
- **SolrExpress.IDocumentCollection\<TDocument\>** dropped, use concret class **DocumentCollection\<TDocument\>** instead
- **SolrExpress.Search.ISolrSearch** dropped, use concret class **DocumentSearch** instead
- **SolrExpress.Update.ISolrAtomicUpdate** dropped, use concret class **DocumentUpdate** instead
- **SolrExpress.DocumentCollection\<TDocument\>** removed property **Engine**
- **SolrExpress.SolrFieldAttribute** removed properties **Indexed**, **Stored**, **OmitNorms**
- **SolrExpress.Search.ISearchParameter** moved to **SolrExpress.Search.Parameter**
- **SolrExpress.Search.Parameter.IFacetFieldParameter** removed method Config. Use methods instead
- **SolrExpress.Search.Parameter.IFacetFieldParameter** rename property **Expression** to **FieldExpression**
- **SolrExpress.Search.Parameter.IFacetRangeParameter** rename property **Expression** to **FieldExpression**
- **SolrExpress.Search.Parameter.IAnyParameter** removed method Config. Use methods instead
- **SolrExpress.Search.Parameter.IFacetLimitParameter** removed method Config. Use methods instead
- **SolrExpress.Search.Parameter.IFacetRangeParameter** removed method Config. Use methods instead
- **SolrExpress.Search.Parameter.IFacetQueryParameter** removed method Config. Use methods instead
- **SolrExpress.Search.Parameter.IBoostParameter** removed method Config. Use methods instead
- **SolrExpress.Search.ISearchParameterValue** moved to **SolrExpress.Search.Parameter**
- **ISearchParameterValue** renamed to **ISearchQuery**
- **ISearchParameterValue** removed property ExpressionBuilder. Use extension methods and SearchParameterQueryBuilder instead
- **SolrExpress.Search.Parameter.IFacetSpatialParameter** removed method Config. Use methods instead
- **SolrExpress.Search.Parameter.IFieldsParameter** removed method Config. Use methods instead
- **SolrExpress.Search.Parameter.IFilterParameter** removed method Config. Use methods instead
- **SolrExpress.Search.Parameter.ILimitParameter** removed method Config. Use methods instead
- **SolrExpress.Search.Parameter.IMinimumShouldMatchParameter** removed method Config. Use methods instead
- **SolrExpress.Search.Parameter.IOffsetParameter** removed method Config. Use methods instead
- **SolrExpress.Search.Parameter.IQueryFieldParameter** removed method Config. Use methods instead
- **SolrExpress.Search.Parameter.IQueryParameter** removed method Config. Use methods instead
- **SolrExpress.Search.Parameter.IRandomSortParameter** removed method Config. Use methods instead
- **SolrExpress.Search.Parameter.ISortParameter** removed method Config. Use methods instead
- **SolrExpress.Search.Parameter.ISpatialFilterParameter** removed method Config. Use methods instead
- **SolrExpress.Core.Search.ParameterValue.SolrSpatialFunctionType** moved to **SolrExpress.Core.Search**
- **SolrSpatialFunctionType** renamed to **SpatialFunctionType**
- **SolrExpress.Search.Parameter.IFieldsParameter** rename property **Expressions** to **FieldExpressions**
- IRandomSortParameter renamed to ISortRandomlyParameter
- BoostFunctionType moved to **SolrExpress.Search**
- FacetSortType moved to **SolrExpress.Search**
