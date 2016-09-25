namespace TicketingSystem.Web.Infrastructure.Services.Contracts
{
    using System.Collections.Generic;

    using ViewModels.Home;

    public interface IHomeService
    {
        IList<TicketViewModel> GetIndexViewModel(int numberOfTickets);
    }
}