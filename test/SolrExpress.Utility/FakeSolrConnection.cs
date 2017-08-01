using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace SolrExpress.Utility
{
    public class FakeSolrConnection : ISolrConnection
    {
        string ISolrConnection.Get(string handler, Dictionary<string, string> data)
        {
            if (handler.StartsWith("schema/fields/"))
            {
                var json = @"
                {
                    ""field"": {
                        ""name"": """",
                        ""indexed"": true,
                        ""stored"": true
                    }
                }";

                var jObject = JObject.Parse(json);
                jObject["field"]["name"] = handler.Split('/')[2];

                return jObject.ToString();
            }

            throw new NotImplementedException();
        }

        string ISolrConnection.GetX(string handler, object data)
        {
            throw new NotImplementedException();
        }

        string ISolrConnection.Post(string handler, string data)
        {
            throw new NotImplementedException();
        }
    }
}
