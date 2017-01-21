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

- **SolrExpress.Search.Parameter.IFacetFieldParameter** removed method Config. Use extension methods instead

- **SolrExpress.Search.Parameter.IFacetFieldParameter** rename property **Expression** to **FieldExpression**

- 

- 

- 