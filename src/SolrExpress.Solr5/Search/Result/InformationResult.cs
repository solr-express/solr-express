using Newtonsoft.Json;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Result;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Solr5.Search.Result
{
    /// <summary>
    /// Information about search result
    /// </summary>
    public class InformationResult<TDocument> : IInformationResult<TDocument>
        where TDocument : Document
    {
        private bool _withElapsedTime;
        private bool _withDocumentCount;

        public Information Data { get; } = new Information();

        public void Execute(IList<ISearchParameter> searchParameters, JsonToken currentToken, string currentPath, JsonReader jsonReader)
        {
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

            if (this._withElapsedTime && this._withDocumentCount)
            {
                var offsetParameter = (IOffsetParameter<TDocument>)searchParameters.First(q => q is IOffsetParameter<TDocument>);
                var limitParameter = (ILimitParameter<TDocument>)searchParameters.First(q => q is ILimitParameter<TDocument>);
                var cursorMarkParameter = (ICursorMarkParameter)searchParameters.FirstOrDefault(q => q is ICursorMarkParameter);
                var offset = offsetParameter.Value;
                var limit = limitParameter.Value;

                this.Data.PageSize = limit;

                if (cursorMarkParameter == null)
                {
                    if (limit > 0)
                    {
                        this.Data.PageNumber = offset / limit + 1;
                        this.Data.PageCount = this.Data.DocumentCount > 0 ? (int)Math.Ceiling(this.Data.DocumentCount / (double)limit) : 0;
                        this.Data.HasPreviousPage = this.Data.PageNumber > 1;
                        this.Data.HasNextPage = this.Data.PageNumber < this.Data.PageCount;
                        this.Data.IsFirstPage = this.Data.PageNumber == 1;
                        this.Data.IsLastPage = this.Data.PageNumber >= this.Data.PageCount;
                    }
                    else
                    {
                        this.Data.PageNumber = 0;
                        this.Data.PageCount = 0;
                        this.Data.HasPreviousPage = false;
                        this.Data.HasNextPage = false;
                        this.Data.IsFirstPage = true;
                        this.Data.IsLastPage = true;
                    }
                }
            }

            // ReSharper disable once InvertIf
            if (currentToken == JsonToken.PropertyName && currentPath == "nextCursorMark")
            {
                var cursorMarkParameter = (ICursorMarkParameter)searchParameters.First(q => q is ICursorMarkParameter);

                this.Data.NextCursorMark = jsonReader.ReadAsString();
                this.Data.HasNextPage = !this.Data.NextCursorMark.Equals(cursorMarkParameter.CursorMark);
                this.Data.IsLastPage = this.Data.NextCursorMark.Equals(cursorMarkParameter.CursorMark);
            }
        }
    }
}