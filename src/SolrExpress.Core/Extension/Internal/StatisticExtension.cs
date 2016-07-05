using SolrExpress.Core.Query.Parameter;
using SolrExpress.Core.Query.Result;
using System;
using System.Collections.Generic;
using System.Linq;

//TODO: Create unit tests
namespace SolrExpress.Core.Extension.Internal
{
    /// <summary>
    /// Extension class used to manipulate Statistic
    /// </summary>
    internal static class StatisticExtension
    {
        internal static Statistic Calculate(this Statistic statistic, List<IParameter> parameters, int elapsedTimeInMilliseconds, long documentCount)
        {
            var offsetParameter = (IOffsetParameter)parameters.FirstOrDefault(q => q is IOffsetParameter);
            var limitParameter = (ILimitParameter)parameters.FirstOrDefault(q => q is ILimitParameter);

            // TODO: Create exception to validate these parameters

            var offset = offsetParameter.Value + 1;
            var limit = limitParameter.Value;

            statistic = new Statistic
            {
                ElapsedTime = new TimeSpan(0, 0, 0, 0, elapsedTimeInMilliseconds),
                DocumentCount = documentCount,
                PageNumber = offset,
                PageSize = limit,
                PageCount = documentCount > 0 ? (int)Math.Ceiling(documentCount / (double)limit) : 0
            };

            statistic.HasPreviousPage = statistic.PageNumber > 1;
            statistic.HasNextPage = statistic.PageNumber < statistic.PageCount;
            statistic.IsFirstPage = statistic.PageNumber == 1;
            statistic.IsLastPage = statistic.PageNumber >= statistic.PageCount;

            return statistic;
        }
    }
}
