using SolrExpress;
using System;

namespace SimpleUse.NetCore
{
    public class TechProduct : Document
    {
        public string Name { get; set; }

        public string Manufacturer { get; set; }
        
        public string ManufacturerId { get; set; }

        public string[] Categories { get; set; }

        public string[] Features { get; set; }

        public decimal Price { get; set; }

        public decimal Popularity { get; set; }

        public bool InStock { get; set; }

        public DateTime ManufacturedateIn { get; set; }

        public GeoCoordinate StoredAt { get; set; }
    }
}
