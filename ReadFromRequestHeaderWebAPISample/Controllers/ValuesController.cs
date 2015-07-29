using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ReadFromRequestHeaderWebAPISample.Controllers
{
  
    public class ValuesController : ApiController
    {   
        string LoginKey { get { return GetLoginKey(); }}

        // GET api/values
        public IHttpActionResult GetHeaderInfo()
        {
            return Content(HttpStatusCode.OK, LoginKey);
        }

        private string GetLoginKey()
        {
            var request = Request;
            var headers = request.Headers;
            if (!headers.Contains("LoginKey")) return null;
            var loginKey = headers.GetValues("LoginKey").First();
            return loginKey;
        }

    }
}
