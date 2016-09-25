namespace TicketingSystem.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Models;

    public class TicketingSystemDbContext : IdentityDbContext<User>
    {
        public TicketingSystemDbContext()
                : base("TicketingSystemConnectionString", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }

        public virtual IDbSet<Image> Images { get; set; }

        public virtual IDbSet<Ticket> Tickets { get; set; }

        public static TicketingSystemDbContext Create()
        {
            return new TicketingSystemDbContext();
        }
    }
}
