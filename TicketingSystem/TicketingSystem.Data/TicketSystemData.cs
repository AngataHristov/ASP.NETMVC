namespace TicketingSystem.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using Models;

    public class TicketSystemData : ITicketSystemData
    {
        private readonly DbContext context;

        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public TicketSystemData(DbContext context)
        {
            this.context = context;
        }

        public IRepository<User> Users => this.GetRepository<User>();

        public IRepository<Ticket> Tickets => this.GetRepository<Ticket>();

        public IRepository<Category> Categories => this.GetRepository<Category>();

        public IRepository<Comment> Comments => this.GetRepository<Comment>();

        public IRepository<Image> Images => this.GetRepository<Image>();

        public DbContext Context => this.context;

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.context?.Dispose();
            }
        }

        private IRepository<T> GetRepository<T>()
            where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);

                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }
    }
}
