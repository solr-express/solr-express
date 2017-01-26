using SolrExpress.Search.Parameter;
using SolrExpress.Search.Result.Utility;
using SolrExpress.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Search.Result
{
    /// <summary>
    /// Information about search result
    /// </summary>
    public class InformationResult<TDocument> : IInformationResult<TDocument>
        where TDocument : IDocument
    {
        Information IInformationResult<TDocument>.Data { get; set; }

        void ISearchResult.Execute(List<ISearchParameter> searchParameters, string jsonPlainText)
        {
            var matchElapsedTimeFragment = SearchResultRegex.InformationResultElapsedTimeragment.Match(jsonPlainText);
            var matchNumFoundFragment = SearchResultRegex.InformationResultDocumentCountFragment.Match(jsonPlainText);

            Checker.IsNull(searchParameters);
            Checker.IsNullOrWhiteSpace(jsonPlainText);
            //TODO: Create exception
            //Checker.IsTrue<UnexpectedJsonFormatException>(matchTimeFragment.Success, new[] { jsonPlainText });
            //Checker.IsTrue<UnexpectedJsonFormatException>(matchNumFoundFragment.Success, new[] { jsonPlainText });

            var offsetParameter = (IOffsetParameter<TDocument>)searchParameters.First(q => q is IOffsetParameter<TDocument>);
            var limitParameter = (ILimitParameter<TDocument>)searchParameters.First(q => q is ILimitParameter<TDocument>);
            var offset = offsetParameter.Value;
            var limit = limitParameter.Value;

            var elapsedTime = Convert.ToInt32(matchElapsedTimeFragment.Groups[3].Value);
            var documentCount = Convert.ToInt64(matchNumFoundFragment.Groups[3].Value);

            var data = new Information
            {
                ElapsedTime = TimeSpan.FromMilliseconds(elapsedTime),
                DocumentCount = documentCount,
                PageNumber = (offset / limit) + 1,
                PageSize = limit,
                PageCount = documentCount > 0 ? (int)Math.Ceiling(documentCount / (double)limit) : 0
            };

            data.HasPreviousPage = data.PageNumber > 1;
            data.HasNextPage = data.PageNumber < data.PageCount;
            data.IsFirstPage = data.PageNumber == 1;
            data.IsLastPage = data.PageNumber >= data.PageCount;

            ((IInformationResult<TDocument>)this).Data = data;
        }
    }
}
