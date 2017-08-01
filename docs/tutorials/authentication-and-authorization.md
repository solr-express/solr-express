# Authentication and Authorization

To use this authentication to connect with SOLR server, just active feature in SolrOptions

''' csharp
    var options = new SolrExpressOptions
    {
        HostAddress = "http://localhost:8983/solr/techproducts",
        Security = new SecurityOptions
        {
            AuthenticationType = AuthenticationType.Basic,
            Password = "<YOUR PASSWORD>",
            UserName = "<YOUR USER NAME>"
        }
    };

    services
		.AddSolrExpress<TechProduct>(builder => builder
			.UseOptions(options) // <-- Use options with this method
			.UseSolr5());
'''

**NOTE**

See more in **[SOLR wiki](https://cwiki.apache.org/confluence/display/solr/Authentication+and+Authorization+Plugins)**;