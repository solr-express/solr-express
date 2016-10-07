# Fail fast

Fail fast feature use **indexed** and **stored** properties of **SolrFieldAttribute** to validate use of fiels and throws exceptions if a not allowed use has made.

To use this feature, follow bellow steps:

1. Active featue in SolrOptions (actived by default):

'''
    // Also, you can use ConfigueOptions
    var options = new DocumentCollectionOptions
    {
        FailFast = true // <-- This make the magic happens
    };
'''

2. Configure SolrFieldAttribute

```
public class TechProductDocument : IDocument
{
    [SolrFieldAttribute("some_field1", Indexed = false, Stored = true)]
    public string SomeField1 { get; set; }

    [SolrFieldAttribute("some_field2", Indexed = true, Stored = false)]
    public string SomeField2 { get; set; }
}
```

Now, some things will occur when use **SomeField**, see bellow:

|Use case|Using method   |SomeField1      |SomeField2|
|--------|---------------|----------------|----------|
|Faceting|FacetQuery(...)|Throws exception|Works well|
|Faceting|FacetRange(...)|Throws exception|Works well|
|Faceting|FacetField(...)|Throws exception|Works well|
|Search  |Query(...)     |Throws exception|Works well|


**NOTE**

See more in **[field options by use case](http://wiki.apache.org/solr/FieldOptionsByUseCase)**;