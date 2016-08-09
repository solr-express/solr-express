using SolrExpress.Core;
using System;

namespace SolrExpress.Benchmarks
{
    public class TestDocument : IDocument
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
        public string Balance { get; set; }
        public long Age { get; set; }
        public string EyeColor { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string About { get; set; }
        public DateTime Registered { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Greeting { get; set; }
        public string FavoriteFruit { get; set; }
    }
}
