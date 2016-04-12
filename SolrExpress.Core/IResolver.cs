using SolrExpress.Core.Parameter;
using SolrExpress.Core.Result;

namespace SolrExpress.Core
{
    /// <summary>
    /// Signatures of classes dependency resolver
    /// </summary>
    public interface IResolver
    {
        TParameter GetParameter<TParameter>()
            where TParameter : class, IParameter;

        TResult GetResult<TResult>()
            where TResult : class, IResult;
    }
}
