using Microsoft.AspNetCore.Mvc;
using SolrExpress.Extension;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;

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
            documentSearch
                .FacetField(q => q.Categories, facet => facet.Excludes("xpto"))
                .Filter(q => q.Categories, 1, 2, 3);

            return new string[] { "value1", "value2" };
        }
    }
}
