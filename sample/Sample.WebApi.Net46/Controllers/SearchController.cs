using Microsoft.AspNetCore.Mvc;
using SolrExpress.Core;
using SolrExpress.Core.Extension;
using System.Collections.Generic;

namespace Sample.WebApi.Net46.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        private IDocumentCollection<TechProduct> _documentCollection;

        public SearchController(IDocumentCollection<TechProduct> documentCollection)
        {
            this._documentCollection = documentCollection;
        }

        [HttpGet]
        public IEnumerable<TechProduct> Get()
        {
            IEnumerable<TechProduct> documents;

            this._documentCollection
                .Select()
                .QueryAll()
                .Limit(3)
                .Execute()
                .Document(out documents);

            return documents;
        }
    }
}
