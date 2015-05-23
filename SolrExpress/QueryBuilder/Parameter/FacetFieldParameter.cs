using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace SolrExpress.QueryBuilder.Parameter
{
    ///// <summary>
    ///// Add facet field parameter to the query
    ///// </summary>
    ///// <param name="fieldName">Name of the field added in the query</param>
    ///// <param name="sortType">Sort type of the result of the facet</param>
    ///// <param name="sortAscending">Sort ascending the result of the facet</param>
    ///// <returns>Itself</returns>
    //public SolrQueryable<TDocument> FacetField(string fieldName, SolrFacetSortType sortType = SolrFacetSortType.Name, bool sortAscending = true)
    //{
    //    var typeName = sortType == SolrFacetSortType.Name ? "index" : "count";
    //    var sortName = sortAscending ? "asc" : "desc";

    //    var array = new List<JProperty>();
    //    array.Add(new JProperty("field", fieldName));
    //    array.Add(new JProperty("sort", new JObject(new JProperty(typeName, sortName))));

    //    var facet = new JProperty(fieldName, new JObject(new JProperty("terms", new JObject(array.ToArray()))));
    //    this.AddParameter(SolrParameterType.FacetField, facet);

    //    return this;
    //}

    public class FacetFieldParameter : IQueryParameter
    {
        private List<FacetFieldItemParameter> _itemns = new List<FacetFieldItemParameter>();

        /// <summary>
        /// Parameter name
        /// </summary>
        public string ParameterName { get { return "facet"; } }

        /// <summary>
        /// Execute the creation of the parameter "filter"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var jArray = new JArray();

            this._itemns.ForEach(q => jArray.Add(q.Execute()));

            jObject[this.ParameterName] = jArray;
        }

        /// <summary>
        /// Add the value to the parameter facet
        /// </summary>
        /// <param name="item">Parameter to include in the query</param>
        /// <returns>It self</returns>
        public FacetFieldParameter Add(FacetFieldItemParameter item)
        {
            this._itemns.Add(item);

            return this;
        }
    }
}
