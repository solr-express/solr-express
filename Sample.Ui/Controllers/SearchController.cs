using Microsoft.AspNet.Mvc;
using Sample.Ui.Context;
using SolrExpress.Core;
using SolrExpress.Core.Extension;
using SolrExpress.Core.Query.ParameterValue;
using SolrExpress.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Ui.Controllers
{
    public class SearchController : Controller
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
        public object Get(int page, string keyWord)
        {
            using (var ctx = new SolrContext())
            {
                List<TechProduct> documents;
                List<FacetKeyValue<string>> facetFieldList;
                Dictionary<string, long> facetQueryList;
                List<FacetKeyValue<FacetRange>> facetRangeList;
                Statistic statistics;

                const int itemsPerPage = 10;

                ctx
                    .TechProducts
                    .Select
                    .QueryField("name^13~3 manu^8~2 id^5")
                    .Query(keyWord ?? "*")
                    .Limit(itemsPerPage)
                    .Offset(page)
                    .FacetField(q => q.Manufacturer)
                    .FacetField(q => q.InStock)
                    .FacetRange("Price", q => q.Price, "10", "10", "100")
                    .FacetRange("Popularity", q => q.Popularity, "1", "1", "10")
                    .FacetRange("ManufacturedateIn", q => q.ManufacturedateIn, "+1MONTH", "NOW-10YEARS", "NOW")
                    .FacetQuery("StoreIn1000km", new Spatial<TechProduct>(SolrSpatialFunctionType.Geofilt, q => q.StoredAt, new GeoCoordinate(35.0752M, -97.032M), 1000M))
                    .Execute()
                    .Document(out documents)
                    .Statistic(out statistics)
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
                        statistics.ElapsedTime,
                        statistics.DocumentCount,
                        pageCount = Math.Ceiling((decimal)statistics.DocumentCount / itemsPerPage)
                    }
                };

                return resul;
            }
        }
    }
}
