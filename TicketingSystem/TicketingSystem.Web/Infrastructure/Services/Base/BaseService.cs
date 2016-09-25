namespace TicketingSystem.Web.Infrastructure.Services.Base
{
    using Data;

    public abstract class BaseService
    {
        public BaseService(ITicketSystemData data)
        {
            this.Data = data;
        }

        protected ITicketSystemData Data { get; private set; }
    }
}