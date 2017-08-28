# Dynamic field behaviour

## Feature

Use dynamic fields controlled by you in code

Useful when use a unified collection and isolate your tenant by a prefix/suffix in each field

## How to

1. You need prepare your SOLR collection with dynamic fields, to do this, follow steps in **[SOLR wiki](https://cwiki.apache.org/confluence/display/solr/Dynamic+Fields)**

2. Choose one or more approachs:

	2.1. Using global settings (see </tutorials/global-settings> to more informations), than all searchs will use **prefix** and/or **suffix** setted in global settings (useful when you know your tenant in app startup)

    2.1.1. Set global settings

	```csharp
		services
			.AddSolrExpress<TechProduct>(builder => builder
				.UseOptions(options =>
				{
					// ... Other settings
					options.GlobalDynamicFieldPrefix = "my_prefix_",
					options.GlobalDynamicFieldSuffix = "_my_suffix"
				})
				// ...  Other settings
				);
	```

    2.1.2. Optionally, set global settings for specific field
    
	```csharp
		using SolrExpress.Search.Behaviour.Extension;
    
		services.AddSolrExpress<TechProduct>(q => q
			.ChangeDynamicFieldBehaviour(q => q.Manufacturer, prefixName: "my_prefix_", suffixName: "_my_suffix")
    		// ...  Other settings
    		);
	```

    2.1.3. Change your class that represent your collection, extend **Document** (see </tutorials/getting-started> to more informations), set dynamic field in **SolrFieldAttribute**, like below:
    
	```csharp
	public class MyDocument : Document
	{
		[SolrField("myfield_s", IsDynamicField = true)]
		public string MyField { get; set; }
	}
	```

    2.2. Using settings to each search, than only this search will use **prefix** and/or **suffix** (useful when you know your tenant only in after some process)
    
    2.2.1. Set settings in your search
    
	```csharp
		using SolrExpress.Search.Behaviour.Extension;
    
		DocumentSearch<TechProduct> documentSearch; // From your DI provider
		documentSearch
			.ChangeDynamicFieldBehaviour(q => q.Manufacturer, prefixName: "my_prefix_", suffixName: "_my_suffix")
    		// ...  Other settings
			.Execute();
    
	```
    2.2.2. Change your class that represent your collection, extend **Document** (see </tutorials/getting-started> to more informations), set dynamic field in **SolrFieldAttribute**, like below:
    
	```csharp
		public class MyDocument : Document
		{
			[SolrField("myfield_s", IsDynamicField = true)]
			public string MyField { get; set; }
		}
	```
    
    2.3. Using settings to each field, than all searchs will use **prefix** and/or **suffix** setted in field settings (useful when you know your tenant in hard code)
    
    2.3.1. Change your class that represent your collection, extend **Document** (see </tutorials/getting-started> to more informations), set settings in **SolrFieldAttribute**, like below:
    
	```csharp
		public class MyDocument : Document
		{
			[SolrField("myfield_s", IsDynamicField = true, DynamicFieldPrefixName = "my_prefix_", DynamicFieldSuffixName = "_my_suffix")]
			public string MyField { get; set; }
		}
	```

** NOTE **

See another example in issue **[206](https://github.com/solr-express/solr-express/issues/206#issuecomment-294005085)**;