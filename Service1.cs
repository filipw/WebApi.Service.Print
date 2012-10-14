using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace WebApi.Service.Print
{
    public partial class PrintApiService : ServiceBase
    {
        private HttpSelfHostServer _server;
        private readonly HttpSelfHostConfiguration _config;
        public const string ServiceAddress = "http://localhost:599";

        public PrintApiService()
        {
            InitializeComponent();

            _config = new HttpSelfHostConfiguration(ServiceAddress);
            _config.Routes.MapHttpRoute("Default",
                "{controller}/{id}",
                new { controller = "Start", id = RouteParameter.Optional });
        }

        protected override void OnStart(string[] args)
        {
            _server = new HttpSelfHostServer(_config);
            _server.OpenAsync();
        }

        protected override void OnStop()
        {
            _server.CloseAsync().Wait();
            _server.Dispose();
        }
    }
}
