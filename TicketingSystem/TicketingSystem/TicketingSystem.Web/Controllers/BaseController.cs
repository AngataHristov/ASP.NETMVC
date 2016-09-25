namespace TicketingSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Microsoft.AspNet.Identity;

    using Data;
    using Models;

    [HandleError]
    public abstract class BaseController : Controller
    {
        public BaseController(ITicketSystemData data)
        {
            this.Data = data;
        }

        protected ITicketSystemData Data { get; private set; }

        protected User UserProfile { get; private set; }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            if (this.User != null)
            {
            this.UserProfile = this.Data
                .Users
                .All()
                .FirstOrDefault(u => u.UserName == requestContext.HttpContext.User.Identity.Name);
            }

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}