using SearchUI.Context;
using SearchUI.Models;
using SolrExpress.Core.Entity;
using SolrExpress.Core.ParameterValue;
using SolrExpress.Solr5.Linq;
using SolrExpress.Solr5.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SearchUI.Controllers
{
    public class SearchController : ApiController
    {
        private List<FacetKeyValue<string>> GetFacetRangeViewModelList(List<FacetKeyValue<FacetRange>> facetRangeList)
        {
            return facetRangeList
                .Select(q =>
                {
                    var item = new FacetKeyValue<string>();
                    item.Name = q.Name;
                    item.Data = new Dictionary<string, long>();

                    foreach (var data in q.Data)
                    {
                        var value = string.Format("{0} - {1}", data.Key.GetMinimumValue() ?? "?", data.Key.GetMaximumValue() ?? "?");
                        item.Data.Add(value, data.Value);
                    }

                    return item;
                })
                .ToList();
        }

        [HttpGet()]
        public IHttpActionResult Get(int page, string keyWord)
        {
            using (var ctx = new SolrContext())
            {
                List<TechProduct> documents;
                List<FacetKeyValue<string>> facetFieldList;
                Dictionary<string, long> facetQueryList;
                List<FacetKeyValue<FacetRange>> facetRangeList;
                bool isEmpty;
                long documentCount;
                TimeSpan timeToExecution;

                const int itemsPerPage = 5;

                ctx.TechProducts
                    .Parameter(new QueryFieldParameter("name^13~3 manu^8~2 id^5"))
                    .Query(keyWord ?? "*.*")
                    .Limit(itemsPerPage)
                    .Offset(page)
                    .FacetField(q => q.Manufacturer)
                    .FacetField(q => q.InStock)
                    .FacetRange("Price", q => q.Price, "10", "10", "100")
                    .FacetRange("Popularity", q => q.Popularity, "1", "1", "10")
                    .FacetRange("ManufacturedateIn", q => q.ManufacturedateIn, "+1MONTH", "NOW-10YEARS", "NOW")
                    .FacetQuery("StoreIn1000km", new FreeValue("{!geofilt sfield=store pt=35.0752,-97.032 d=1000}"))
                    .Execute()
                    .Document(out documents)
                    .Statistic(out isEmpty, out documentCount, out timeToExecution)
                    .FacetField(out facetFieldList)
                    .FacetQuery(out facetQueryList)
                    .FacetRange(out facetRangeList);

                var resul = new
                {
                    documents,
                    facets = new
                    {
                        field = facetFieldList,
                        query = facetQueryList,
                        range = this.GetFacetRangeViewModelList(facetRangeList)
                    },
                    statistic = new
                    {
                        timeToExecution,
                        documentCount,
                        pageCount = Math.Ceiling((decimal)documentCount / itemsPerPage)
                    }
                };

                return Ok(resul);
            }
        }
    }
}
