using System;

namespace SolrExpress.Benchmarks
{
    public class TestDocument : Document
    {
        [SolrField("isActive")]
        public bool IsActive { get; set; }

        [SolrField("balance")]
        public string Balance { get; set; }

        [SolrField("age")]
        public long Age { get; set; }

        [SolrField("eyeColor")]
        public string EyeColor { get; set; }

        [SolrField("name")]
        public string Name { get; set; }

        [SolrField("gender")]
        public string Gender { get; set; }

        [SolrField("company")]
        public string Company { get; set; }

        [SolrField("email")]
        public string Email { get; set; }

        [SolrField("phone")]
        public string Phone { get; set; }

        [SolrField("address")]
        public string Address { get; set; }

        [SolrField("about")]
        public string About { get; set; }

        [SolrField("registered")]
        public DateTime Registered { get; set; }

        [SolrField("latitude")]
        public decimal Latitude { get; set; }

        [SolrField("longitude")]
        public decimal Longitude { get; set; }

        [SolrField("greeting")]
        public string Greeting { get; set; }

        [SolrField("favoriteFruit")]
        public string FavoriteFruit { get; set; }
    }
}