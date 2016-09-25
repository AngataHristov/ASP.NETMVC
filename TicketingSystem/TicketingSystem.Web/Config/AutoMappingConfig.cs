namespace TicketingSystem.Web
{
    using System.Reflection;

    using TicketSystem.Web.Infrastructure.Mapping;

    public static class AutoMappingConfig
    {
        public static void Config()
        {
            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(Assembly.GetExecutingAssembly());
        }
    }
}

