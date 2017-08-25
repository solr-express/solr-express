# Authentication and Authorization

## Feature

Use authentication to connect with SOLR server

## How to

1. You need activate this feature in SOLR server, to do this, follow steps in **[SOLR wiki](https://cwiki.apache.org/confluence/display/solr/Authentication+and+Authorization+Plugins)**

2. Change your **SolrExpressOptions** and set **SecurityOptions**, like below:

''' csharp
    var options = new SolrExpressOptions
    {
        // ... Other settings
        Security = new SecurityOptions
        {
            AuthenticationType = AuthenticationType.Basic,
            Password = "<YOUR PASSWORD>",
            UserName = "<YOUR USER NAME>"
        }
    };

    services
		.AddSolrExpress<TechProduct>(builder => builder
			.UseOptions(options) // <-- Use options
			// ...  Other settings
			);
'''