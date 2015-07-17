using System.Web.Http;

namespace SearchUI.Controllers
{
    public class DefaultController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok("Test");
        }
    }
}
