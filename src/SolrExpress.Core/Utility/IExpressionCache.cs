using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolrExpress.Core.Utility
{
    public interface IExpressionCache<TDocument>
        where TDocument : IDocument
    {
        void Process();
    }
}
