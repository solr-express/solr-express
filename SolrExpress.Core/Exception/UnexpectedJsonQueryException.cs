using System.Text;

namespace SolrExpress.Core.Exception
{
    public class UnexpectedJsonQueryException : System.Exception
    {
        /// <summary>
        /// Get the exception message
        /// </summary>
        /// <param name="json">Unexpected JSON string</param>
        /// <returns>Exception message</returns>
        private static string GetExceptionMessage(string json)
        {
            var sb = new StringBuilder();
            sb.AppendLine("The informed JSON string is unexpected in the use of the query");
            sb.AppendLine("The informed JSON string was:");
            sb.AppendLine(json);

            return sb.ToString();
        }

        /// <summary>
        /// Default constructor of the class
        /// </summary>
        /// <param name="json">Unexpected JSON string</param>
        public UnexpectedJsonQueryException(string json)
            : base(UnexpectedJsonQueryException.GetExceptionMessage(json))
        {
        }
    }
}
