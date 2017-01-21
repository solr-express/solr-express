using Microsoft.AspNetCore.Mvc;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using System;

namespace SimpleUse.SingleContext.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        [HttpGet]
        public object Get()
        {
            IFacetFieldParameter<TechProduct> parameter = null;

            parameter.FieldExpression(q => q.Categories);


            DocumentSearch<TechProduct> documentSearch = null;
            documentSearch.FacetField(q => q.Categories, facet => facet.Excludes("xpto"));

            return new string[] { "value1", "value2" };
        }
    }
}
