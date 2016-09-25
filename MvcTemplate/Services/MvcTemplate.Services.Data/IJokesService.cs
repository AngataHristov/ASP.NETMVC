namespace MvcTemplate.Services.Data
{
    using System.Linq;

    using Common.Constants;
    using MvcTemplate.Data.Models;

    public interface IJokesService
    {
        IQueryable<Joke> GetRandomJokes(int count = ValidationConstants.DefaultNumberOfRandomJokes);

        Joke GetById(string id);
    }
}
