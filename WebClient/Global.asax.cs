using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using Autofac;
using Autofac.Integration.Mvc;
using NServiceBus;
using Client;
using Client.Service;


public class MvcApplication :
    HttpApplication
{
    IEndpointInstance endpoint;

    static void RegisterRoutes(RouteCollection routes)
    {
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

        routes.MapRoute(
            "Default", // Route name
            "{controller}/{action}/{id}", // URL with parameters
            new
            {
                controller = "Equipment",
                action = "List",
                id = UrlParameter.Optional
            } // Parameter defaults
            );
    }

    protected void Application_End()
    {
        if (endpoint != null)
        {
            endpoint.Stop().GetAwaiter().GetResult();
        }
    }

    protected void Application_Start()
    {
        #region ApplicationStart

        var builder = new ContainerBuilder();

        // Register the MVC controllers.
        builder.RegisterControllers(typeof(MvcApplication).Assembly);

        // Set the dependency resolver to be Autofac.
        var container = builder.Build();

        DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        var endpointConfiguration = new EndpointConfiguration("Samples.Mvc.WebApplication");
        endpointConfiguration.MakeInstanceUniquelyAddressable("1");
        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.UseSerialization<JsonSerializer>();
        endpointConfiguration.UseContainer<AutofacBuilder>(
            customizations: customizations =>
            {
                customizations.ExistingLifetimeScope(container);
            });
        endpointConfiguration.UsePersistence<InMemoryPersistence>();
        endpointConfiguration.EnableInstallers();

        endpoint = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();

        var updater = new ContainerBuilder();
        updater.RegisterInstance(endpoint);
        updater.RegisterControllers(typeof(MvcApplication).Assembly);
        var equipmentProvider = new EquipmentProvider(endpoint);
        updater.RegisterInstance<IEquipmentProvider>(equipmentProvider);
        var updated = updater.Build();

        DependencyResolver.SetResolver(new AutofacDependencyResolver(updated));

        AreaRegistration.RegisterAllAreas();
        RegisterRoutes(RouteTable.Routes);

        BundleConfig.RegisterBundles(BundleTable.Bundles);

        ViewEngines.Engines.Add(new RazorViewEngine());

        #endregion
    }
}