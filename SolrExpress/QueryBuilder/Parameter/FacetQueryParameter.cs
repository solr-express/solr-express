using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolrExpress.QueryBuilder.Parameter
{
    ///// <summary>
    ///// Add facet query parameter to the query
    ///// </summary>
    ///// <param name="aliasName">Name of the alias added in the query</param>
    ///// <param name="query">Query used to make the facet</param>
    ///// <param name="sortType">Sort type of the result of the facet</param>
    ///// <param name="sortAscending">Sort ascending the result of the facet</param>
    ///// <returns>Itself</returns>
    //public SolrQueryable<TDocument> FacetQuery(string aliasName, string query, SolrFacetSortType sortType = SolrFacetSortType.Name, bool sortAscending = true)
    //{
    //    var typeName = sortType == SolrFacetSortType.Name ? "index" : "count";
    //    var sortName = sortAscending ? "asc" : "desc";

    //    var array = new List<JProperty>();
    //    array.Add(new JProperty("q", query));
    //    array.Add(new JProperty("sort", new JObject(new JProperty(typeName, sortName))));

    //    var facet = new JProperty(aliasName, new JObject(new JProperty("query", new JObject(array.ToArray()))));
    //    this.AddParameter(SolrParameterType.FacetQuery, facet);

    //    return this;
    //}

    class FacetQueryParameter
    {
    }
}
