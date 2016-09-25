namespace TicketingSystem.Web.Infrastructure.Services
{
    using System.Linq;

    using Base;
    using Contracts;
    using Data;
    using Models;

    public class TicketsService : BaseService, ITicketsService
    {
        public TicketsService(ITicketSystemData data)
            : base(data)
        {
        }

        public IQueryable<Ticket> GetAllTickets()
        {
            return this.Data.Tickets
                    .All();
        }

        public IQueryable<Ticket> GetTicketById(int id)
        {
            return this.Data
                    .Tickets
                    .All()
                    .Where(t => t.Id == id);
        }

        public IQueryable<Comment> GetCommentsByTicketId(int id)
        {
            return this.Data
                .Comments
                .All()
                .Where(c => c.TicketId == id)
                .OrderByDescending(c => c.Id);
        }
    }
}