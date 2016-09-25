namespace TicketingSystem.Web.Infrastructure.Services.Contracts
{
    using System.Linq;

    using Models;

    public interface ITicketsService
    {
        IQueryable<Ticket> GetAllTickets();

        IQueryable<Ticket> GetTicketById(int id);

        IQueryable<Comment> GetCommentsByTicketId(int id);
    }
}