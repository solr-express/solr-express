using System;

namespace SolrExpress.Connection
{
    public class CustomSolrConnectionAuthenticationNotFoundException : Exception
    {
        public CustomSolrConnectionAuthenticationNotFoundException() :
            base(Resource.CustomSolrConnectionAuthenticationNotFoundException)
        {
        }
    }
}
