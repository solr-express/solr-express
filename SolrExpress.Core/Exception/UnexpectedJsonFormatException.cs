using System.Text;

namespace SolrExpress.Core.Exception
{
    public class UnexpectedJsonFormatException : System.Exception
    {
        /// <summary>
        /// Get the exception message
        /// </summary>
        /// <param name="json">Unexpected JSON string</param>
        /// <returns>Exception message</returns>
        private static string GetExceptionMessage(string json)
        {
            var sb = new StringBuilder();
            sb.AppendLine("The parameter was not found in the source json");
            sb.AppendLine("The parameter was:");
            sb.AppendLine(json);

            return sb.ToString();
        }

        /// <summary>
        /// Default constructor of the class
        /// </summary>
        /// <param name="json">Unexpected JSON string</param>
        public UnexpectedJsonFormatException(string json)
            : base(UnexpectedJsonFormatException.GetExceptionMessage(json))
        {
        }
    }
}
