namespace WebApi.Service.Print.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;
    using System.Diagnostics;
    using System.Web.Http;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Net;
    using System.Security;
    using System.Threading.Tasks;

    public class PrintController : ApiController
    {
        public Task<HttpResponseMessage> Post()
        {
            var rootUrl = "c:/PrintService/Uploads/";

            if (Request.Content.IsMimeMultipartContent())
            {
                var streamProvider = new CustomMultipartFormDataStreamProvider(rootUrl);
                var task = Request.Content.ReadAsMultipartAsync(streamProvider).ContinueWith<HttpResponseMessage>(t =>
                    {
                        if (t.IsFaulted || t.IsCanceled)
                            throw new HttpResponseException(HttpStatusCode.InternalServerError);

                        t.Result.FileData.ToList().ForEach(i =>
                        {
                            var info = new FileInfo(i.LocalFileName);
                            Print(info.FullName);
                        });

                        return new HttpResponseMessage(HttpStatusCode.OK);
                    });
                return task;
            }
            
            throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
        }

        private void Print(string path)
        {
            if (File.Exists(path))
            {
                var printJob = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = path,
                        UseShellExecute = true,
                        Verb = "print",
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        WorkingDirectory = Path.GetDirectoryName(path)
                    }
                };

                printJob.Start();
            }
        }
    }
}
