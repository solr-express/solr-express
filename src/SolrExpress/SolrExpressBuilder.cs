using System;

namespace SolrExpress
{
    public sealed class SolrExpressBuilder<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Create a new instance of T using internal DI engine
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Instance of T</returns>
        public T Create<T>()
        {
            throw new NotImplementedException();
        }
    }
}