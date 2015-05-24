
namespace SolrExpress.Solr5.Builder
{
    /// <summary>
    /// Represents a Facet Range without knowledgement of the type of the minumum and maximum values
    /// </summary>
    public class FacetRange
    {
        /// <summary>
        /// Key of the range of the facet
        /// </summary>
        string Key { get; set; }
    }

    /// <summary>
    /// Represents a Facet Range with knowledgement of the type of the minumum and maximum values
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FacetRange<T> : FacetRange
    {
        /// <summary>
        /// Minimum value of the facet
        /// </summary>
        T MinimumValue { get; set; }

        /// <summary>
        /// Maximum value of the facet
        /// </summary>
        T MaximumValue { get; set; }
    }
}
