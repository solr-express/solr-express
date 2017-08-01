# Fail fast

Fail fast feature use **indexed** and **stored** properties of **SolrFieldAttribute** to validate using of fields in some features throwing exception (SearchParameterIsInvalidException) if necesary.

To use this feature, just active feature in SolrOptions (actived by default)

''' csharp
    var options = new SolrExpressOptions
    {
        HostAddress = "http://localhost:8983/solr/techproducts",
		FailFast = true
    };

    services
		.AddSolrExpress<TechProduct>(builder => builder
			.UseOptions(options) // <-- Use options with this method
			.UseSolr5());
'''

Using a collection with 2 fields with follow setting:

| Field  |Indexed |Stored |
|--------|--------|-------|
| Field1 | False  | True  |
| Field2 | True   | False |

Now, some things will occur when use fields, explinated bellow:

| Use case     | Using method | Field1           | Field2           |
|--------------|--------------|------------------|------------------|
| Faceting     | FacetField   | Throws exception | Works well       |
| Faceting     | FacetQuery   | Throws exception | Works well       |
| Faceting     | FacetRange   | Throws exception | Works well       |
| Filtering    | Filter       | Throws exception | Works well       |
| Get contents | Fields       | Works well       | Throws exception |
| Search       | Query        | Throws exception | Works well       |
| Sorting      | Sort         | Throws exception | Works well       |

**NOTE**

See more in **[field options by use case](http://wiki.apache.org/solr/FieldOptionsByUseCase)**;