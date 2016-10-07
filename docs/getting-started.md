# Getting started

To start to  use SolrExpress, just follow bellow steps:

1. Make sure than you have Solr runnning;
    2. Create a class to represent your collection, and implements **_IDocument_** interface and use attributes to indicate Solr fields. Like this
    ```
    public class TechProductDocument : IDocument
    {
        [SolrFieldAttribute("id", Indexed = false, Stored = true)]
        public string Id { get; set; }

        [SolrFieldAttribute("manu", Indexed = true, Stored = true)]
        public string Manufacturer { get; set; }

        [SolrFieldAttribute("store", Indexed = true, Stored = true)]
        public GeoCoordinate StoredAt { get; set; }
    }
    ```
3. Configure Dependecy Injection (only if you use Net.Core), OR configure builder (only if you use Net 4.0/4.5) 
    ```
    #if NETCOREAPP1.0
    // In Startup.cs
    public void ConfigureServices(IServiceCollection services)
    {
        // Also, you can use ConfigueOptions
        var options = new DocumentCollectionOptions
        {
            CheckAnyParameter = true,
            FailFast = true
        };

        services.AddSolrExpress<TechProduct>(builder => builder
            .UseOptions(options)
            .UseHostAddress("http://localhost:8983/solr/techproducts")
            .UseSolr5());
    }
    #endif
    ```

    ```
    #if #if NET40 || NET45
    private IDocumentCollection<TechProduct> _techProducts;
    public void ConfigureCollection()
    {
        this._techProducts = new DocumentCollectionBuilder<TechProduct>()
            .AddSolrExpress()
            .UseHostAddress("http://localhost:8983/solr/techproducts")
            .UseSolr5()
            .Create();
    }
    #endif
    ```
4. Configue search parameters, execute and read results and enjoy :)
    ```
    public void MyAmazingSearch()
    {
        this._techProducts
            .Select()
            .QueryField("name^13~3 manu^8~2 id^5")
            .Query(keyWord ?? "*")
            .Limit(itemsPerPage)
            .Offset(page)
            .FacetField(q => q.Manufacturer)
            .FacetField(q => q.InStock)
            .FacetRange("Price", q => q.Price, "10", "10", "100")
            .FacetRange("Popularity", q => q.Popularity, "1", "1", "10")
            .FacetRange("ManufacturedateIn", q => q.ManufacturedateIn, "+1MONTH", "NOW-10YEARS", "NOW")
            .FacetQuery("StoreIn1000km", new Spatial<TechProduct>(
                SolrSpatialFunctionType.Geofilt,
                q => q.StoredAt, new GeoCoordinate(35.0752M, -97.032M),
                1000M))
            .Execute()
            .Document(out documents)
            .Information(out statistics)
            .FacetField(out facetFieldList)
            .FacetQuery(out facetQueryList)
            .FacetRange(out facetRangeList);
    }
    ```

**NOTES**

1. The class **_TechProductDocument_** represents a document in Solr collection and to be easy to identitify this, I call this class with the same name of the Solr collection (techproducts) but you can choose any name what you want, just remember, implements **_IDocument_** interface;
2. Framework uses NewtonSoft to parse json result and you can call your properties with a better name. (i.e. Property _Manufacturer_ represents Solr field _manu_); 
3. If you want use fail fast feature (activated by default), you need pass some information about Solr fiels to SolrFieldAttribute. Indexed and stored will be used in validation and throws exceptions depending of the use of the fields. See more in **[Fail fast](/tutorials/fail-fast)**;
4. To example purposes, I set collection address in hard code.