namespace TicketingSystem.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;
    using Data;
    using Web.Controllers;
    using Common;

    [Authorize(Roles =GlobalConstants.AdminRole)]
    public abstract class AdminController : BaseController
    {
        public AdminController(ITicketSystemData data)
            : base(data)
        {
        }
    }
}