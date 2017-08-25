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
        private bool _executed;
        private bool _withElapsedTime;
        private bool _withDocumentCount;

        public Information Data { get; } = new Information();

        public void Execute(IList<ISearchParameter> searchParameters, JsonToken currentToken, string currentPath, JsonReader jsonReader)
        {
            if (this._executed)
            {
                return;
            }

            if (currentToken == JsonToken.PropertyName && currentPath == "responseHeader.QTime")
            {
                this.Data.ElapsedTime = TimeSpan.FromMilliseconds(jsonReader.ReadAsDouble().Value);

                this._withElapsedTime = true;
            }

            if (currentToken == JsonToken.PropertyName && currentPath == "response.numFound")
            {
                jsonReader.Read();
                this.Data.DocumentCount = (long)jsonReader.Value;

                this._withDocumentCount = true;
            }

            // ReSharper disable once InvertIf
            if (this._withElapsedTime && this._withDocumentCount)
            {
                var offsetParameter = (IOffsetParameter<TDocument>)searchParameters.First(q => q is IOffsetParameter<TDocument>);
                var limitParameter = (ILimitParameter<TDocument>)searchParameters.First(q => q is ILimitParameter<TDocument>);
                var offset = offsetParameter.Value;
                var limit = limitParameter.Value;

                var data = this.Data;
                if (limit > 0)
                {
                    data.PageNumber = offset / limit + 1;
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

                this._executed = true;
            }
        }
    }
}