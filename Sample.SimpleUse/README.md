## Solr Server
First of all, you need startup a Solr Server and configure a collection, to do this, follow instructions in [this link](https://github.com/solr-express/solr-express/blob/master/SolrExpress.Solr5.IntegrationTests/read.md)

## Basic use

Step by step to use the framework

* Create a class and implement the IDocument interface

```csharp
	public class TechProductDocument : IDocument
    {
        public string Id { get; set; }

        [SolrFieldAttribute("manu")]
        public string Manufacturer { get; set; }
		
		[SolrFieldAttribute("store", Indexed = true, Stored = true, OmitNorms = true)]
        public GeoCoordinate StoredAt { get; set; }
	}
```

**Notes:**

1. This class represents a document in Solr collection.

2. To be easy to identitify this, I call this class with the same name of the Solr collection (techproducts) 
	but you can choose any name what you want

3. The framework uses NewtonSoftware to parse json result from Solr because this, you can change the case of the properties

4. In the cases than you want a name more cool or a name than represents your bussiness logic, you can use SolrFieldAttribute to indicates this name

5. If you want use fail fast feature (activated by default), you need pass some information about field properties to the SolrFieldAttribute.
	pProperties indexed, stored, omitNorms will be used in validation and throws exceptions depending of the use of the fields

* Create a instance of DocumentCollection, IoC/DI Resolver and Confiruation classes

```csharp
    public class SolrContext : IDisposable
    {
        public SolrContext()
        {
            var provider = new Provider("http://localhost:8983/solr/techproducts");
            var resolver = new SimpleResolver().Configure();
            var configuration = new Configuration();

            this.TechProducts = new DocumentCollection<TechProduct>(provider, resolver, configuration);
        }

        public DocumentCollection<TechProduct> TechProducts { get; private set; }

        public void Dispose()
        {
        }
    }
```
**Notes:**

1. I use a context to isolate the DocumentCollection creation, but you can use a factory to do this

2. To example purposes, I set collection address in hard code.

* Use parameters

```csharp
var ctx = new SolrContext();
var select = ctx.TechProducts.Select.Query(new QueryAll());
```

* Execute the query

```csharp
var result = select.Execute();
```

* And get results

```csharp
List<TechProduct> documents;
result.Document(out documents);
```