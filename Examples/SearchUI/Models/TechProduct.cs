using SolrExpress.Core.Attribute;
using SolrExpress.Core.Entity;
using SolrExpress.Core.Query;
using System;

namespace SearchUI.Models
{
    public class TechProduct : IDocument
    {
        [SolrFieldAttribute("id", Indexed = true, Stored = true)]
        public string Id { get; set; }

        [SolrFieldAttribute("name", Indexed = true, Stored = true)]
        public string Name { get; set; }

        [SolrFieldAttribute("manu", Indexed = true, Stored = true)]
        public string Manufacturer { get; set; }

        [SolrFieldAttribute("manu_id_s", Indexed = true, Stored = true)]
        public string ManufacturerId { get; set; }

        [SolrFieldAttribute("cat", Indexed = true, Stored = true)]
        public string[] Categories { get; set; }

        [SolrFieldAttribute("features", Indexed = true, Stored = true)]
        public string[] Features { get; set; }

        [SolrFieldAttribute("price", Indexed = true, Stored = true)]
        public decimal Price { get; set; }

        [SolrFieldAttribute("popularity", Indexed = true, Stored = true)]
        public decimal Popularity { get; set; }

        [SolrFieldAttribute("inStock", Indexed = true, Stored = true)]
        public bool InStock { get; set; }

        [SolrFieldAttribute("manufacturedate_dt", Indexed = true, Stored = true)]
        public DateTime ManufacturedateIn { get; set; }

        [SolrFieldAttribute("store", Indexed = true, Stored = true)]
        public GeoCoordinate StoredAt { get; set; }
    }
}