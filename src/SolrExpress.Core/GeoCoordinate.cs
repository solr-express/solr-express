using SolrExpress.Core.Utility;
using System.Globalization;

namespace SolrExpress.Core
{
    public struct GeoCoordinate
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="latitude">The latitude of the location</param>
        /// <param name="longitude">The longitude of the location</param>
        public GeoCoordinate(decimal latitude, decimal longitude)
        {
            Checker.IsOutOfRange(latitude, -90, 90, Resource.InvalidLatitudeException);
            Checker.IsOutOfRange(longitude, -180, 180, Resource.InvalidLongitudeException);

            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        /// <summary>
        /// The latitude of the location. May range from -90.0 to 90.0.
        /// </summary>
        public decimal Latitude { get; set; }

        /// <summary>
        /// The longitude of the location. May range from -180.0 to 180.0.
        /// </summary>
        public decimal Longitude { get; set; }

        /// <summary>
        /// Returns the fully qualified type name of this instance
        /// </summary>
        /// <returns>A System.String containing a fully qualified type name</returns>
        public override string ToString()
        {
            return $"{this.Latitude.ToString("G", CultureInfo.InvariantCulture)},{this.Longitude.ToString("G", CultureInfo.InvariantCulture)}";
        }
    }
}
