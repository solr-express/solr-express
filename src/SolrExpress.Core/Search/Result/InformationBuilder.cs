using SolrExpress.Core.Search.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Core.Search.Result
{
    /// <summary>
    /// Builder class used to manipulate Information
    /// </summary>
    internal class InformationBuilder<TDocument>
        where TDocument : IDocument
    {
        internal static Information Create(IEnumerable<ISearchParameter> parameters, int elapsedTimeInMilliseconds, long documentCount)
        {
            var offsetParameter = (IOffsetParameter<TDocument>)parameters.First(q => q is IOffsetParameter<TDocument>);
            var limitParameter = (ILimitParameter<TDocument>)parameters.First(q => q is ILimitParameter<TDocument>);

            var offset = offsetParameter.Value;
            var limit = limitParameter.Value;

            var information = new Information
            {
                ElapsedTime = new TimeSpan(0, 0, 0, 0, elapsedTimeInMilliseconds),
                DocumentCount = documentCount,
                PageNumber = (offset / limit) + 1,
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
