using SolrExpress.Core.Attribute;
using SolrExpress.Core.Entity;
using SolrExpress.Core.Query;
using System;

namespace BasicUse
{
    public class TechProduct : IDocument
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [SolrFieldAttribute("manu")]
        public string Manufacturer { get; set; }

        [SolrFieldAttribute("manu_id_s")]
        public string ManufacturerId { get; set; }

        [SolrFieldAttribute("cat")]
        public string[] Categories { get; set; }

        public string[] Features { get; set; }

        public decimal Price { get; set; }

        public decimal Popularity { get; set; }

        public bool InStock { get; set; }

        [SolrFieldAttribute("manufacturedate_dt")]
        public DateTime ManufacturedateIn { get; set; }

        [SolrFieldAttribute("store", Indexed = true, Stored = true, OmitNorms = true)]
        public GeoCoordinate StoredAt { get; set; }
    }
}
