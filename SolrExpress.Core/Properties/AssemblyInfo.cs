using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Diego Luiz Brum")]
[assembly: AssemblyCopyright("Copyright ©  2015")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: Guid("05f1c1bd-b1b6-4e82-931f-7882973b24f6")]
[assembly: AssemblyVersion("2.1.0.1")]
[assembly: AssemblyFileVersion("2.1.0.1")]

#if STRONGNAME
[assembly: AssemblyTitle("SolrExpress.Core.Signed")]
[assembly: AssemblyProduct("SolrExpress.Core.Signed")]
[assembly: AssemblyDescription("Core library for a simple and lightweight query .NET library for Solr (Signed version)")]
[assembly: AssemblyKeyFile("SolrExpress.Core.snk")]
[assembly: InternalsVisibleTo("SolrExpress.Solr4.Signed, PublicKey=0024000004800000940000000602000000240000525341310004000001000100db586ada300d1954b1b6c83ec3cb4b624dfa290eef45d239ef4fcafce953f0dadf175032f73ea1e8e9b76232a14065bcfc422461244dd2bab66041be540ef5fc52387aa7e669da1f273c61c048831523be8c2c9ecdf0157b6afedc060f382ab4f34a54fe107ff7cc0c6695e891c9837e5cb23f07fa2fc04658f10548c3dc3be9")]
[assembly: InternalsVisibleTo("SolrExpress.Solr5.Signed, PublicKey=00240000048000009400000006020000002400005253413100040000010001008b8455aace84cfd4e671ff79c06d750462de84756c41c729c3dfb19ec19de0dfe7dc8fa156417aaa5cf6cbb75dd2ca2bd3750e462d8765bff95fd6cc5a81046fd24fd02c43aa4b0eb44019f328ca1997561ba9f3a9fd9f8505bed8bf8b371d638c937890c4e55302fa4dd8441e677dd2ac42f6844226188a85f526ca1a195ea7")]
[assembly: InternalsVisibleTo("SolrExpress.Core.Tests, PublicKey=0024000004800000940000000602000000240000525341310004000001000100d98b048815007fb0d996997bcc2edcfe4b4c04c407999a21b3e1ff73b069ab1a99f6d184d4e9805e67beee1a342b3ad4d502acfe1029b7080ca34f939bc98836de9f68dc60d2b59d6fcbd38f910381f293c16d9e61da783ff7bee65c73d6602116a91e08dbb9fd3dbd11894e0609be1dd3eb7b0794fa6dec4b8751785df306ae")]
#else
[assembly: AssemblyTitle("SolrExpress.Core")]
[assembly: AssemblyProduct("SolrExpress.Core")]
[assembly: AssemblyDescription("Core library for a simple and lightweight query .NET library for Solr")]
[assembly: InternalsVisibleTo("SolrExpress.Solr4")]
[assembly: InternalsVisibleTo("SolrExpress.Solr5")]
[assembly: InternalsVisibleTo("SolrExpress.Core.Tests")]
#endif