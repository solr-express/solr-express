using System;

namespace SolrExpress.Core
{
    public sealed class UnsupportedSecuritySystemException : Exception
    {
        public UnsupportedSecuritySystemException() :
            base(Resource.UnsupportedSecuritySystemException)
        {
        }
    }
}
