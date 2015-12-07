using SolrExpress.Core.Attribute;
using SolrExpress.Core.Entity;
using SolrExpress.Core.Query;
using System;

namespace SearchUI.Models
{
    public class TechProduct : IDocument
    {
        [SolrField("id", Indexed = true, Stored = true)]
        public string Id { get; set; }

        [SolrField("name", Indexed = true, Stored = true)]
        public string Name { get; set; }

        [SolrField("manu", Indexed = true, Stored = true)]
        public string Manufacturer { get; set; }

        [SolrField("manu_id_s", Indexed = true, Stored = true)]
        public string ManufacturerId { get; set; }

        [SolrField("cat", Indexed = true, Stored = true)]
        public string[] Categories { get; set; }

        [SolrField("features", Indexed = true, Stored = true)]
        public string[] Features { get; set; }

        [SolrField("price", Indexed = true, Stored = true)]
        public decimal Price { get; set; }

        [SolrField("popularity", Indexed = true, Stored = true)]
        public decimal Popularity { get; set; }

        [SolrField("inStock", Indexed = true, Stored = true)]
        public bool InStock { get; set; }

        [SolrField("manufacturedate_dt", Indexed = true, Stored = true)]
        public DateTime ManufacturedateIn { get; set; }

        [SolrField("store", Indexed = true, Stored = true)]
        public GeoCoordinate StoredAt { get; set; }
    }
}