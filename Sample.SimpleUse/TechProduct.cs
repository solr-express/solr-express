using SolrExpress.Core;
using System;

namespace Sample.SimpleUse
{
    public class TechProduct : IDocument
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [SolrField("manu")]
        public string Manufacturer { get; set; }

        [SolrField("manu_id_s")]
        public string ManufacturerId { get; set; }

        [SolrField("cat")]
        public string[] Categories { get; set; }

        public string[] Features { get; set; }

        public decimal Price { get; set; }

        public decimal Popularity { get; set; }

        public bool InStock { get; set; }

        [SolrField("manufacturedate_dt")]
        public DateTime ManufacturedateIn { get; set; }

        [SolrField("store", Indexed = true, Stored = true, OmitNorms = true)]
        public GeoCoordinate StoredAt { get; set; }
    }
}
