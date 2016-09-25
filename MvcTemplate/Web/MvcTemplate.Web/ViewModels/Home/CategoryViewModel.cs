namespace MvcTemplate.Web.ViewModels.Home
{
    using Data.Models;
    using Infrastructure.Mapping;

    public class CategoryViewModel : IMapFrom<JokeCategory>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}