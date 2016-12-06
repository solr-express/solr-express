using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolrExpress.Core.Utility
{
    public class ExpressionCache<TDocument> : IExpressionCache<TDocument>
        where TDocument : IDocument
    {
        void IExpressionCache<TDocument>.Process()
        {
            throw new NotImplementedException();
        }
    }
}
