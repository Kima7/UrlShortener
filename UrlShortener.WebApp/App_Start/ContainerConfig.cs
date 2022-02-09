using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using UrlShortener.Common.Interfaces;
using UrlShortener.Common.Repository;
using UrlShortener.Services.Repository;
using UrlShortener.Services.Services;

namespace UrlShortener.WebApp.App_Start
{
    public class ContainerConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //services
            builder.RegisterType<UrlService>().As<IUrlService>();
            builder.RegisterType<UrlShortenerService>().As<IUrlShortenerService>();

            //repo
            builder.RegisterType<UrlRepository>().As<IUrlRepository>();

            var container = builder.Build();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}