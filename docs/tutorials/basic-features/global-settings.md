# Global settings

## Feature

Set settings only once to all searchs using global settings

Useful when use same parameters to all searchs

## How to

1. In your services configurations (used to add Solr Express services in DI provider), invoke method **UseOptions**

```csharp
	services
		.AddSolrExpress<TechProduct>(builder => builder
			.UseOptions(options =>
            {
                // ... Global settings
            })
			// ...  Other settings
			);
```

2. Set options with necessary parameter, field behaviour or some other feature provided by Solr Express