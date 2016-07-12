using SolrExpress.Core.Query.Parameter;
using SolrExpress.Core.Query.Result;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Core.Extension.Internal
{
    /// <summary>
    /// Extension class used to manipulate Statistic
    /// </summary>
    internal static class InformationExtension
    {
        internal static Information Calculate(this Information information, List<IParameter> parameters, int elapsedTimeInMilliseconds, long documentCount)
        {
            var offsetParameter = (IOffsetParameter)parameters.First(q => q is IOffsetParameter);
            var limitParameter = (ILimitParameter)parameters.First(q => q is ILimitParameter);
            
            var offset = offsetParameter.Value + 1;
            var limit = limitParameter.Value;

            information = new Information
            {
                ElapsedTime = new TimeSpan(0, 0, 0, 0, elapsedTimeInMilliseconds),
                DocumentCount = documentCount,
                PageNumber = offset,
                PageSize = limit,
                PageCount = documentCount > 0 ? (int)Math.Ceiling(documentCount / (double)limit) : 0
            };

            information.HasPreviousPage = information.PageNumber > 1;
            information.HasNextPage = information.PageNumber < information.PageCount;
            information.IsFirstPage = information.PageNumber == 1;
            information.IsLastPage = information.PageNumber >= information.PageCount;

            return information;
        }
    }
}
