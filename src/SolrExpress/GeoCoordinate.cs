using SolrExpress.Utility;
using System;
using System.Globalization;

namespace SolrExpress
{
    public struct GeoCoordinate : IEquatable<GeoCoordinate>
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
        /// Indicates whether the current object is equal to another object of the same type
        /// </summary>
        /// <param name="other">An object to compare with this object</param>
        /// <returns>True if the current object is equal to the other parameter; otherwise, false.</returns>
        public bool Equals(GeoCoordinate other)
        {
            return
                this.Latitude.Equals(other.Latitude) &&
                this.Longitude.Equals(other.Longitude);
        }

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
