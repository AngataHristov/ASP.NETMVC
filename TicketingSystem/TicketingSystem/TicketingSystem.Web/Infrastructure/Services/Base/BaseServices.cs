namespace TicketingSystem.Web.Infrastructure.Services.Base
{
    using Data;

    public abstract class BaseServices
    {
        public BaseServices(ITicketSystemData data)
        {
            this.Data = data;
        }

        protected ITicketSystemData Data { get; private set; }
    }
}