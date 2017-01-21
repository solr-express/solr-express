# Use cases

# DRAFT

---

## Parameters

Allows send parameters to Sorl in a controlled and buildable way.

Using **IDocumentCollection\<TechProduct\>** provided by framework via DI and put this in a variable called **techProducts** and invoke mathod **Select()**.

### Use mode #1

All parameters have a common mode of use (i.e. Facet field using only field.name).

To use this form, just use IDocumentCollection extension methods

|-------------|-----------------------|
| Parameter   | Configure using       |
|-------------|-----------------------|
| Facet field | FacetField(q => q.Id) |

### Use mode #2

A common case is configure some properties to have a custom behaviour in SOLR searchs (i.e. Facet field with Excludes)

To use this form, just use a mix of IDocumentCollection and IFacetFieldParameter extension methods

|-------------|----------------------------------------------------------------|
| Parameter   | Configure using                                                |
|-------------|----------------------------------------------------------------|
| Facet field | FacetField(q => q.Categories, facet => facet.Excludes("xpto")) |

### Use mode #3

In some cases, it's necessary create a parameter inside a Command and return add this to IDocumentCollection

To use this form, just create a parameter using SolrExpressBuilder class, configure created parameter and append this using Add method.

```
SolrExpressBuilder builder; // <= Get this from your DI engine
IDocumentCollection\<TechProduct\> documentCollection; // <= Get this from your DI engine

var facet = builder.Create<IFacetFieldParameter\<TechProduct\>>();
facet
	.FieldExpression(q => q.Categories)
	.Excludes("xpto");

documentCollection
	.Select()
	.Add(facet);
```