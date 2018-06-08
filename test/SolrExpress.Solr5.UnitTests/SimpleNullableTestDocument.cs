using System;

namespace SolrExpress.Solr5.UnitTests
{
    public class SimpleNullableTestDocument : Document
    {
        [SolrField("date")]
        public DateTime Date { get; set; }

        [SolrField("dateNullable")]
        public DateTime? DateNullable { get; set; }

        [SolrField("coord")]
        public GeoCoordinate GeoCoordinate { get; set; }

        [SolrField("coordNullable")]
        public GeoCoordinate? GeoCoordinateNullable { get; set; }
    }
}
