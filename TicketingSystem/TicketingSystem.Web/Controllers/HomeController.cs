namespace TicketingSystem.Web.Controllers
{
    using System.Web.Mvc;

    using Data;
    using Infrastructure.Services.Contracts;

    public class HomeController : BaseController
    {
        private readonly IHomeService homeService;

        public HomeController(ITicketSystemData data, IHomeService homeService)
            : base(data)
        {
            this.homeService = homeService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Error()
        {
            return View();
        }

        [ChildActionOnly]
        [OutputCache(Duration = 60 * 60)]
        public ActionResult MostCommentedTickets()
        {
            var indexViewModel = this.homeService
                .GetIndexViewModel(6);

            return this.PartialView("_MostCommentedTicketsPartial", indexViewModel);
        }
    }
}