using System.Globalization;

namespace SolrExpress.Core.Query
{
    public struct GeoCoordinate
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="latitude">The latitude of the location</param>
        /// <param name="longitude">The longitude of the location</param>
        public GeoCoordinate(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        /// <summary>
        /// The latitude of the location. May range from -90.0 to 90.0.
        /// </summary>
        public double Latitude;

        /// <summary>
        /// The longitude of the location. May range from -180.0 to 180.0.
        /// </summary>
        public double Longitude;

        /// <summary>
        /// Returns a string that represents the current coordinate
        /// </summary>
        /// <returns>A string that represents the current coordinate</returns>
        public override string ToString()
        {
            return string.Concat(
                this.Latitude.ToString("0.0", CultureInfo.InvariantCulture),
                ",",
                this.Longitude.ToString("0.0", CultureInfo.InvariantCulture));
        }
    }
}
