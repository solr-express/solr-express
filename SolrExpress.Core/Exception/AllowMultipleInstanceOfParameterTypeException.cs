namespace SolrExpress.Core.Exception
{
    public class AllowMultipleInstanceOfParameterTypeException : System.Exception
    {
        public AllowMultipleInstanceOfParameterTypeException(string parameterType) :
            base(string.Format(Resource.AllowMultipleInstanceOfParameterTypeException, parameterType))
        {
        }
    }
}
