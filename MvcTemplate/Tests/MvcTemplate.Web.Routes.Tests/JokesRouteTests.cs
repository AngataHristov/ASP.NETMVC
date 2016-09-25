namespace MvcTemplate.Web.Routes.Tests
{
    using System;
    using System.Web.Routing;

    using Controllers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MvcRouteTester;

    [TestClass]
    public class JokesRouteTests
    {
        [TestMethod]
        public void TestRouteById()
        {
            const string Url = "/Joke/Mjc2NS4xMjMxMjMxMzEyMw==";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);

            routeCollection
                .ShouldMap(Url)
                .To<JokesController>(c => c.ById("Mjc2NS4xMjMxMjMxMzEyMw=="));
        }
    }
}
