namespace TicketingSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Data;
    using ViewModels.Home;
    using TicketSystem.Web.Infrastructure.Mapping;
    using Infrastructure.Services.Contracts;

    public class HomeController : BaseController
    {
        private readonly IHomeService homeService;

        public HomeController(ITicketSystemData data, IHomeService homeService)
            : base(data)
        {
            this.homeService = homeService;
        }

        // [OutputCache(Duration = 60 * 60)]
        public ActionResult Index()
        {
            var indexViewModel = this.homeService
                .GetIndexViewModel(6);

            return this.View(indexViewModel);
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}