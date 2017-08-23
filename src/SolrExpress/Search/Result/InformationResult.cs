using Newtonsoft.Json;
using SolrExpress.Search.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Search.Result
{
    /// <summary>
    /// Information about search result
    /// </summary>
    public class InformationResult<TDocument> : IInformationResult<TDocument>
        where TDocument : Document
    {
        private bool executed = false;
        private bool withElapsedTime = false;
        private bool withDocumentCount = false;

        public InformationResult()
        {
            ((IInformationResult<TDocument>)this).Data = new Information();
        }

        Information IInformationResult<TDocument>.Data { get; set; }

        void ISearchResult<TDocument>.Execute(IList<ISearchParameter> searchParameters, JsonToken currentToken, string currentPath, JsonReader jsonReader)
        {
            if (!this.executed)
            {
                if (currentToken == JsonToken.PropertyName && currentPath == "responseHeader.QTime")
                {
                    ((IInformationResult<TDocument>)this).Data.ElapsedTime = TimeSpan.FromMilliseconds(jsonReader.ReadAsDouble().Value);

                    this.withElapsedTime = true;
                }

                if (currentToken == JsonToken.PropertyName && currentPath == "response.numFound")
                {
                    jsonReader.Read();
                    ((IInformationResult<TDocument>)this).Data.DocumentCount = (long)jsonReader.Value;

                    this.withDocumentCount = true;
                }

                if (this.withElapsedTime && this.withDocumentCount)
                {
                    var offsetParameter = (IOffsetParameter<TDocument>)searchParameters.First(q => q is IOffsetParameter<TDocument>);
                    var limitParameter = (ILimitParameter<TDocument>)searchParameters.First(q => q is ILimitParameter<TDocument>);
                    var offset = offsetParameter.Value;
                    var limit = limitParameter.Value;

                    var data = ((IInformationResult<TDocument>)this).Data;
                    if (limit > 0)
                    {
                        data.PageNumber = (offset / limit) + 1;
                        data.PageSize = limit;
                        data.PageCount = data.DocumentCount > 0 ? (int)Math.Ceiling(data.DocumentCount / (double)limit) : 0;
                        data.HasPreviousPage = data.PageNumber > 1;
                        data.HasNextPage = data.PageNumber < data.PageCount;
                        data.IsFirstPage = data.PageNumber == 1;
                        data.IsLastPage = data.PageNumber >= data.PageCount;
                    }
                    else
                    {
                        data.IsFirstPage = true;
                        data.IsLastPage = true;
                    }

                    this.executed = true;
                }
            }
        }
    }
}