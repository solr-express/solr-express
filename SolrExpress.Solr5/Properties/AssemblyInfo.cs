using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("SolrExpress.Solr5")]
[assembly: AssemblyDescription("A simple and lightweight query .NET library for Solr 5.x")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("https://github.com/solr-express/solr-express")]
[assembly: AssemblyProduct("SolrExpress.Solr5")]
[assembly: AssemblyCopyright("Copyright ©  2015")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("f56423f9-becd-45f7-bc39-c3ca6c581780")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.1.0.1")]
[assembly: AssemblyFileVersion("1.1.0.1")]

#if STRONGNAME
[assembly: AssemblyKeyFile("SolrExpress.Solr5.snk")]
[assembly: InternalsVisibleTo("SolrExpress.Solr5.IntegrationTests, PublicKey=00240000048000009400000006020000002400005253413100040000010001004b3f6f17482e418b4b17c9fa96bd5924044793ba0c5f679c0e50c005010e937053b0041ab7911ff4708bfb804d36c4e43fb7c2cfd215e7e83f44c7087bb9bfa0853269f7a7daff7c88e799638e553750d48ef2a1b0bc0bbcec920be40b29998afc4ca2b86dca2cdf4faa16c1ab4bc71ca0d832e2240d034a66f2ae388ddfb8e8")]
[assembly: InternalsVisibleTo("SolrExpress.Solr5.UnitTests, PublicKey=00240000048000009400000006020000002400005253413100040000010001004d083e96fe78f231717fe0fb3da103cc459e51fee8223f1dacd96eaae0afc7f467f80b1231e1030dced331260493179e096ea1d8e304d0aa875c1fb026559bea558b194faf43a0e7c49622bdcdd56ae303e7f63ab149c45e9f13981a0b0c3caaca28646aa131518ddb4d2df5e1c4c7736155976187506007922a4bf4410f85b4")]
#else
[assembly: InternalsVisibleTo("SolrExpress.Solr5.IntegrationTests")]
[assembly: InternalsVisibleTo("SolrExpress.Solr5.UnitTests")]
#endif