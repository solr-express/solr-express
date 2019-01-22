using Newtonsoft.Json;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using System;
using System.Collections.Generic;
using System.IO;

namespace SolrExpress.Benchmarks.Helper
{
    public class FakeSearchItemCollection<TDocument> : ISearchItemCollection<TDocument>
        where TDocument : Document
    {
        public void Add(ISearchItem item)
        {
            // Test purpose only
        }

        public void AddRange(IEnumerable<ISearchItem> items)
        {
            // Test purpose only
        }

        public bool Contains<TSearchItem>() where TSearchItem : ISearchItem
        {
            return false;
        }

        public bool Contains(Type searchItemType)
        {
            return false;
        }

        public JsonReader Execute(string requestHandler)
        {
            return new JsonTextReader(new StringReader("{}"));
        }

        public List<ISearchParameter> GetSearchParameters()
        {
            return new List<ISearchParameter>();
        }

        public List<Search.Result.ISearchResult<TDocument>> GetSearchResults()
        {
            return new List<Search.Result.ISearchResult<TDocument>>();
        }
    }
}
