namespace MvcTemplate.Services.Data
{
    using System;
    using System.Linq;

    using Common.Constants;
    using MvcTemplate.Data.Common.Repositories;
    using MvcTemplate.Data.Models;
    using Web;

    public class JokesService : IJokesService
    {
        private readonly IDbRepository<Joke> jokes;
        private readonly IIdentifierProvider identifierProvider;

        public JokesService(
            IDbRepository<Joke> jokesData,
            IIdentifierProvider identifierProvider)
        {
            this.jokes = jokesData;
            this.identifierProvider = identifierProvider;
        }

        public Joke GetById(string id)
        {
            var idAsInt = this.identifierProvider.DecodeId(id);
            var joke = this.jokes.GetById(idAsInt);

            return joke;
        }

        public IQueryable<Joke> GetRandomJokes(int count = ValidationConstants.DefaultNumberOfRandomJokes)
        {
            var randomJokes = this.jokes
                .All()
                .OrderBy(j => Guid.NewGuid())
                .Take(count);

            return randomJokes;
        }
    }
}
