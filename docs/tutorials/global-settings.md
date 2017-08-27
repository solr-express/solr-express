# Global settings

## Feature

Set settings only once to all searchs using global settings

Useful when use same parameters to all searchs

## How to

1. In your services configurations (used to add Solr Express services in DI provider), create a instance of **SolrExpressOptions**

```csharp
	var options = new SolrExpressOptions();
```

2. Set options with necessary parameter, field behaviour or some other feature provided by Solr Express

3. Use options invoking method **UseOptions**

```csharp
	var options = new SolrExpressOptions();

	// ...  Other settings

	services
		.AddSolrExpress<TechProduct>(builder => builder
			.UseOptions(options) // <-- Use options
			// ...  Other settings
			);
```