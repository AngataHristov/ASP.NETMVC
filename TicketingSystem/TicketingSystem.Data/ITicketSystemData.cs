using System.Data.Entity;
using TicketingSystem.Models;

namespace TicketingSystem.Data
{
    public interface ITicketSystemData
    {
        IRepository<User> Users { get; }

        IRepository<Ticket> Tickets { get; }

        IRepository<Category> Categories { get; }

        IRepository<Comment> Comments { get; }

        IRepository<Image> Images { get; }

        DbContext Context { get; }

        void Dispose();

        int SaveChanges();
    }
}
