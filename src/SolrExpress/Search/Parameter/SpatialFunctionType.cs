namespace SolrExpress.Core.Search.Parameter
{
    /// <summary>
    /// Types of functions used in spatial thingys
    /// </summary>
    public enum SpatialFunctionType
    {
        /// <summary>
        /// Retrieve results based on the geospatial distance (AKA the "great circle distance") 
        /// </summary>
        Geofilt,

        /// <summary>
        /// Retrieve results based on the bounding box of the calculated circle
        /// </summary>
        Bbox
    }
}
