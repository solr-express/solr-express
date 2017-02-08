using Newtonsoft.Json;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Result;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace SolrExpress.UnitTests.Search.Result
{
    public class DocumentResultTests
    {
        [Fact]
        public void InformationResultFact001()
        {
            // Arrange
            var jsonPlainText = @"
            {
              ""responseHeader"": {
                ""status"": 0,
                ""QTime"": 0,
                ""params"": {
                  ""q"": ""*:*"",
                  ""indent"": ""true"",
                  ""fq"": ""manu_id_s:samsung"",
                  ""wt"": ""json""
                }
              },
              ""response"": {
                ""numFound"": 1,
                ""start"": 0,
                ""docs"": [
                  {
                    ""id"": ""SP2514N"",
                    ""name"": ""Samsung SpinPoint P120 SP2514N - hard drive - 250 GB - ATA-133"",
                    ""manu"": ""Samsung Electronics Co. Ltd."",
                    ""manu_id_s"": ""samsung"",
                    ""cat"": [
                      ""electronics"",
                      ""hard drive""
                    ],
                    ""features"": [
                      ""7200RPM, 8MB cache, IDE Ultra ATA-133"",
                      ""NoiseGuard, SilentSeek technology, Fluid Dynamic Bearing (FDB) motor""
                    ],
		              ""docs"": [
                      ""7200RPM, 8MB cache, IDE Ultra ATA-133"",
                      ""NoiseGuard, SilentSeek technology, Fluid Dynamic Bearing (FDB) motor""
                    ],
                    ""price"": 92.0,
                    ""price_c"": ""92.0,USD"",
                    ""popularity"": 6,
                    ""inStock"": true,
                    ""manufacturedate_dt"": ""2006-02-13T15:26:37Z"",
                    ""store"": ""35.0752,-97.032"",
                    ""_version_"": 1557618225908285440
                  },
                  {
                    ""id"": ""__SP2514N__"",
                    ""name"": ""Samsung SpinPoint P120 SP2514N - hard drive - 250 GB - ATA-133"",
                    ""manu"": ""Samsung Electronics Co. Ltd."",
                    ""manu_id_s"": ""samsung"",
                    ""cat"": [
                      ""electronics"",
                      ""hard drive""
                    ],
                    ""features"": [
                      ""7200RPM, 8MB cache, IDE Ultra ATA-133"",
                      ""NoiseGuard, SilentSeek technology, Fluid Dynamic Bearing (FDB) motor""
                    ],
		              ""docs"": [
                      ""7200RPM, 8MB cache, IDE Ultra ATA-133"",
                      ""NoiseGuard, SilentSeek technology, Fluid Dynamic Bearing (FDB) motor""
                    ],
                    ""price"": 92.0,
                    ""price_c"": ""92.0,USD"",
                    ""popularity"": 6,
                    ""inStock"": true,
                    ""manufacturedate_dt"": ""2006-02-13T15:26:37Z"",
                    ""store"": ""35.0752,-97.032"",
                    ""_version_"": 1557618225908285440
                  }
                ]
              }
            }";

            var jsonReader = new JsonTextReader(new StringReader(jsonPlainText));

            var searchParameters = new List<ISearchParameter>();

            var result = (IDocumentResult<TechProductDocument>)new DocumentResult<TechProductDocument>();

            // Act
            while (jsonReader.Read())
            {
                result.Execute(searchParameters, jsonReader.TokenType, jsonReader.Path, jsonReader);
            }

            // Assert
            var list = result.Data.ToList();

            Assert.Equal(2, list.Count);
            Assert.Equal("SP2514N", list[0].Id);
            Assert.Equal("__SP2514N__", list[1].Id);
        }
    }
}
