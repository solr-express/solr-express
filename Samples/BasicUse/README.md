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

4. In the cases what the name is not good to you, and you want a name more cool or a name than represents your bussiness logic,
	you can use SolrFieldAttribute to indicates this name

5. If you want use fail fast feature (activated by default), you need pass some information about field properties to the SolrFieldAttribute.
	The properties indexed, stored, omitNorms will be validate and throws exceptions depending of the use of the fields

* Create a instance of SolrQueryable class and set the Provider instance.

```csharp
	public class SolrContext : IDisposable
    {
        public SolrContext()
        {
            var provider = new Provider("http://localhost:8983/solr/techproducts");

            this.TechProducts = new SolrQueryable<TechProduct>(provider);
        }

        public SolrQueryable<TechProduct> TechProducts { get; private set; }
    }
```
**Notes:**

1. I use a context to isolate the SolrQueryable creation, but you can use a factory to do this

2. To example purposes, I set collection address in hard code but, don't do this in real life.
	Put the information in a config file or some other place

* Use parameters

```csharp
var ctx = new SolrContext();
ctx.TechProducts.Parameter(new QueryParameter(new QueryAll()));
```

* Execute the query

```csharp
var result = ctx.TechProducts.Execute();
```

* And get results

```csharp
var documents = result.Get(new DocumentBuilder<TechProduct>()).Data;
```