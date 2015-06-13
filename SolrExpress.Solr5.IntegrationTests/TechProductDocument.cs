using SolrExpress.Core.Attribute;
using SolrExpress.Core.Query;
using System;

namespace SolrExpress.Solr5.IntegrationTests
{
    public class TechProductDocument : IDocument
    {
        [SolrFieldAttribute("id")]
        public string Id { get; set; }

        [SolrFieldAttribute("name")]
        public string Name { get; set; }

        [SolrFieldAttribute("manu")]
        public string Manufacturer { get; set; }

        [SolrFieldAttribute("manu_id_s")]
        public string ManufacturerId { get; set; }

        [SolrFieldAttribute("cat")]
        public string[] Categories { get; set; }

        [SolrFieldAttribute("features")]
        public string[] Features { get; set; }

        [SolrFieldAttribute("price")]
        public decimal Price { get; set; }

        [SolrFieldAttribute("popularity")]
        public decimal Popularity { get; set; }

        [SolrFieldAttribute("inStock")]
        public bool InStock { get; set; }

        [SolrFieldAttribute("manufacturedate_dt")]
        public DateTime ManufacturedateIn { get; set; }

        [SolrFieldAttribute("store")]
        public GeoCoordinate StoredAt { get; set; }
    }
}