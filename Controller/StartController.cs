namespace WebApi.Service.Print.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Http;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.IO;

    public class StartController : ApiController
    {
        public HttpResponseMessage Get()
        {
            var response = new HttpResponseMessage();
            response.Content = new StringContent(File.ReadAllText(@"c:\PrintService\start.html"));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }
    }
}
