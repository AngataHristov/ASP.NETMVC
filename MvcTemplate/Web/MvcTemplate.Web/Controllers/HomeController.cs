namespace MvcTemplate.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Infrastructure.Mapping;
    using Services.Data;
    using ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly IJokesService jokes;
        private readonly ICategoriesService categories;

        public HomeController(
            IJokesService jokes,
            ICategoriesService categories)
        {
            this.jokes = jokes;
            this.categories = categories;
        }

        public ActionResult Index()
        {
            var jokes = this.jokes
                .GetRandomJokes(10)
                .To<JokeViewModel>()
                .ToList();

            var categories = this.Cache
                .Get(
                    "categories",
                    () =>
                        this.categories
                         .GetAll()
                         .To<CategoryViewModel>()
                         .ToList(),
                    30 * 60);

            var viewModel = new IndexViewModel()
            {
                Jokes = jokes,
                Categories = categories
            };

            return this.View(viewModel);
        }
    }
}