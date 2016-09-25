namespace MvcTemplate.Web
{
    using System.Data.Entity;
    using System.Reflection;
    using System.Web.Mvc;

    using Autofac;
    using Autofac.Integration.Mvc;

    using Controllers;
    using Data;
    using Data.Common.Repositories;
    using Services.Data;
    using Services.Web;

    public static class AutofacConfig
    {
        public static void RegisterAutofac()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // Register services
            RegisterServices(builder);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder
                .Register(dbc => new ApplicationDbContext())
                .As<DbContext>()
                .InstancePerRequest();

            builder
               .Register(idp => new IdentifierProvider())
               .As<IIdentifierProvider>()
               .InstancePerRequest();

            builder
                .RegisterGeneric(typeof(DbRepository<>))
                .As(typeof(IDbRepository<>))
                .InstancePerRequest();

            var servicesAssembly = Assembly
                .GetAssembly(typeof(IJokesService));
            builder
                .RegisterAssemblyTypes(servicesAssembly)
                .AsImplementedInterfaces();

            var servicesAssembly2 = Assembly
                .GetAssembly(typeof(IIdentifierProvider));
            builder
                .RegisterAssemblyTypes(servicesAssembly2)
                .AsImplementedInterfaces();

            builder
                .Register(cs => new HttpCacheService())
                .As<ICacheService>()
                .InstancePerRequest();

            builder
                .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AssignableTo<BaseController>()
                .PropertiesAutowired();
        }
    }
}