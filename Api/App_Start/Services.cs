using Api.Formatters;
using Api.Validators;
using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;

namespace PhonenumberService
{
    public static class Services
    {
        public static void Register(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            // Register API controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Services
            builder.RegisterType<NumberValidator>()
                .InstancePerRequest();

            builder.RegisterType<NumberFormatter>()
                .UsingConstructor(typeof(NumberValidator))
                .InstancePerRequest();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}