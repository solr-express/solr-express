# Fail fast

## Feature

Throw **SearchParameterIsInvalidException** when developer use a field in wrong way

Using a collection with 2 fields with follow settings:

| Field  |Indexed |Stored |
|--------|--------|-------|
| Field1 | False  | True  |
| Field2 | True   | False |

Use cases:

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

## How to

**This feature is actived by default**

To active/inactive this feature, change your **SolrExpressOptions** and set **FailFast**, like below:

```csharp
	services
		.AddSolrExpress<TechProduct>(builder => builder
			.UseOptions(options =>
            {
				// ... Other settings
				options.FailFast = true
            })
			// ...  Other settings
			);
```