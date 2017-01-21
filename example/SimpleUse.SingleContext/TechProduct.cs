using SolrExpress;
using System;

namespace SimpleUse.SingleContext
{
    public class TechProduct : IDocument
    {
        [SolrField("id")]
        public string Id { get; set; }

        [SolrField("name")]
        public string Name { get; set; }

        [SolrField("manu")]
        public string Manufacturer { get; set; }

        [SolrField("manu_id_s")]
        public string ManufacturerId { get; set; }

        [SolrField("cat")]
        public string[] Categories { get; set; }

        [SolrField("features")]
        public string[] Features { get; set; }

        [SolrField("price")]
        public decimal Price { get; set; }

        [SolrField("popularity")]
        public decimal Popularity { get; set; }

        [SolrField("inStock")]
        public bool InStock { get; set; }

        [SolrField("manufacturedate_dt")]
        public DateTime ManufacturedateIn { get; set; }

        [SolrField("store")]
        public GeoCoordinate StoredAt { get; set; }
    }
}
