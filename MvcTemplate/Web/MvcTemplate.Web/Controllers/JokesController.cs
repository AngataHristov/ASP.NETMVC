namespace MvcTemplate.Web.Controllers
{
    using System.Web.Mvc;

    using Infrastructure.Mapping;
    using Services.Data;
    using ViewModels.Home;

    public class JokesController : BaseController
    {
        private readonly IJokesService jokesService;

        public JokesController(IJokesService jokesService)
        {
            this.jokesService = jokesService;
        }

        public ActionResult ById(string id)
        {
            var joke = this.jokesService.GetById(id);

            var viewModel = AutoMapperConfig.Configuration
                .CreateMapper()
                .Map<JokeViewModel>(joke);

            return this.View(viewModel);
        }
    }
}