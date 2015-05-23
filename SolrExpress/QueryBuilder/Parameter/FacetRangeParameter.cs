using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolrExpress.QueryBuilder.Parameter
{
    ///// <summary>
    ///// Add facet range parameter to the query
    ///// </summary>
    ///// <param name="aliasName">Name of the alias added in the query</param>
    ///// <param name="fieldName">Name of the field added in the query</param>
    ///// <param name="gap">Size of each range bucket to make the facet</param>
    ///// <param name="start">Lower bound to make the facet</param>
    ///// <param name="end">Upper bound to make the facet</param>
    ///// <param name="sortType">Sort type of the result of the facet</param>
    ///// <param name="sortAscending">Sort ascending the result of the facet</param>
    ///// <returns>Itself</returns>
    //public SolrQueryable<TDocument> FacetRange(string aliasName, string fieldName, string gap, string start, string end, SolrFacetSortType sortType = SolrFacetSortType.Name, bool sortAscending = true)
    //{
    //    var typeName = sortType == SolrFacetSortType.Name ? "index" : "count";
    //    var sortName = sortAscending ? "asc" : "desc";

    //    var array = new List<JProperty>();
    //    array.Add(new JProperty("field", fieldName));
    //    array.Add(new JProperty("start", start));
    //    array.Add(new JProperty("end", end));
    //    array.Add(new JProperty("gap", gap));
    //    array.Add(new JProperty("other", new JArray("before", "after")));
    //    array.Add(new JProperty("sort", new JObject(new JProperty(typeName, sortName))));

    //    var facet = new JProperty(aliasName, new JObject(new JProperty("range", new JObject(array.ToArray()))));
    //    this.AddParameter(SolrParameterType.FacetQuery, facet);

    //    return this;
    //}

    class FacetRangeParameter
    {
    }
}
