using Api.Formatters;
using Api.Validators;
using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;

namespace PhonenumberService
{
    public class Services
    {
        #region Setup

        public static void Register(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            // Register API controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Services
            RegisterServices(builder);

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        #endregion

        protected static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<NumberValidator>()
                .As<INumberValidator>()
                .InstancePerRequest();

            builder.RegisterType<NumberFormatter>()
                .As<INumberFormatter>()
                .UsingConstructor(typeof(INumberValidator))
                .InstancePerRequest();
        }
    }
}