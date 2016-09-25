namespace TicketingSystem.Web.Infrastructure.Services
{
    using System.Linq;
    using System.Collections.Generic;

    using ViewModels.Home;
    using TicketSystem.Web.Infrastructure.Mapping;
    using Contracts;
    using Base;
    using Data;

    public class HomeService : BaseServices, IHomeService
    {
        public HomeService(ITicketSystemData data)
            : base(data)
        {
        }

        public IList<TicketViewModel> GetIndexViewModel(int numberOfTickets)
        {
            var indexViewModel = this.Data
                .Tickets
                .All()
                .OrderByDescending(t => t.Comments.Count())
                .Take(numberOfTickets)
                .To<TicketViewModel>()
                .ToList();

            return indexViewModel;
        }
    }
}