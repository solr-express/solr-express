using Microsoft.AspNetCore.Mvc;
using SolrExpress.Search;
using SolrExpress.Search.Behaviour.Extension;

namespace SimpleUse.SingleContext.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        [HttpGet]
        public object Get()
        {
            DocumentSearch<TechProduct> documentSearch = null;
            documentSearch
                .ChangeDynamicFieldBehaviour(q => q.Manufacturer, prefixName: "", suffixName: "")
                .Execute();

            return new string[] { "value1", "value2" };
        }
    }
}
