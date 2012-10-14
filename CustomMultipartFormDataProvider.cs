// -----------------------------------------------------------------------
// <copyright file="CustomMultipartFormDataProvider.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace WebApi.Service.Print
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Net.Http;

    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(string path)
            : base(path)
        { }

        public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
        {
            var name = !string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName) ? headers.ContentDisposition.FileName : "NoName";
            return name.Replace("\"", string.Empty); //this is here because Chrome submits files in quotation marks which get treated as part of the filename and get escaped
        }
    }
}
