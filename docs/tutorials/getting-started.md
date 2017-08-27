# Getting started

To start to use SolrExpress, just follow follow steps:

1. Create a class to represent your collection, extend **Document** class and use attributes to indicate Solr fields.

```csharp
	public class TechProductDocument : Document
	{
		[SolrField("manu")]
		public string Manufacturer { get; set; }

		[SolrField("store")]
		public GeoCoordinate StoredAt { get; set; }
	}
```

3. Choose your favorite Dependency Injection provider (SolrExpress.DI.Autofac, SolrExpress.DI.CoreClr, SolrExpress.DI.Ninject or SolrExpress.DI.SimpleInject) and add reference to package
4. Add reference to correct Solr provider (SolrExpress.Solr4 or SolrExpress.Solr5)
5. Configure SolrExpress setting Solr host address for **each** collection

```csharp
	public void ConfigureServices(SomeDIContainer services)
	{
		services
			// This extension is from SolrExpress.DI.<Your favorite DI provider>
			.AddSolrExpress<TechProduct>(builder => builder
				.UseHostAddress("http://localhost:8983/solr/techproducts")
				// This extension is from SolrExpress.Solr5
				.UseSolr5()); // Or UseSolr4 from SolrExpress.Solr4
	}
```

6. Configue search parameters, execute, read results and enjoy :)

```csharp
	public void MyAmazingSearch(DocumentCollection<TechProduct> techProducts)
	{
		// Initial search settings (configure to result facet field Categories and filter by field id using value "205325092")
		var searchResult = techProducts
			.Select()
			.Fields(d => d.Id, d => d.Manufacturer)
			.FacetField(d => d.Categories)
			.Filter(d => d.Id, "205325092")
			.Execute();

        // Get general information about search, documents and facets from search result
        searchResult
            .Information(out var information)
            .Document(out var documents)
            .Facets(out var facets);
	}
```

**NOTES**

1. Class **_TechProductDocument_** represents a document in Solr collection and to be easy to identitify this, I call this class with the same name of the Solr collection (techproducts) but you can choose any name what you want, just remember, extends **_Document_** class;
2. To example purposes, I set collection address in hard code.