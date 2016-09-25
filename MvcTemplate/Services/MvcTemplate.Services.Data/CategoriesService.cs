namespace MvcTemplate.Services.Data
{
    using System;
    using System.Linq;

    using MvcTemplate.Data.Common.Repositories;
    using MvcTemplate.Data.Models;

    public class CategoriesService : ICategoriesService
    {
        private IDbRepository<JokeCategory> categories;

        public CategoriesService(IDbRepository<JokeCategory> data)
        {
            this.categories = data;
        }

        public IQueryable<JokeCategory> GetAll()
        {
            var allCategories = this.categories
                .All()
                .OrderBy(c => c.Name);

            return allCategories;
        }

        public JokeCategory EnsureCategory(string name)
        {
            var category = this.categories
                .All()
                .FirstOrDefault(c => c.Name == name);

            if (category != null)
            {
                return category;
            }

            var newCategory = new JokeCategory()
            {
                Name = name
            };

            this.categories.Add(newCategory);
            this.categories.Save();

            return newCategory;
        }
    }
}
