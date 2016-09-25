namespace MvcTemplate.Web
{
    using System.Web.Mvc;

    public static class ViewEnginesConfig
    {
        public static void Config()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }
    }
}