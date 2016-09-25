namespace TicketingSystem.Web.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;

    using Microsoft.Web.Mvc;

    using Data;
    using Models;
    using TicketSystem.Web.Infrastructure.Mapping;
    using ViewModels.Tickets;
    using ViewModels.Comments;
    using Infrastructure.Populators;
    using Infrastructure.Services.Contracts;

    public class TicketsController : BaseController
    {
        private readonly IDropDownListPopulator populator;
        private readonly ITicketsService ticketService;

        public TicketsController(ITicketSystemData data, IDropDownListPopulator populator, ITicketsService ticketService)
            : base(data)
        {
            this.populator = populator;
            this.ticketService = ticketService;
        }

        [Authorize]
        public ActionResult All(int? category)
        {
            return this.View(category);
        }

        [Authorize]
        [HttpPost]
        public ActionResult ReadTickets([DataSourceRequest] DataSourceRequest request, int? categoryId)
        {
            //var ticketsQuery = this.Data.Tickets
            //    .All()
            //    .AsQueryable();

            var ticketsQuery = this.ticketService
                .GetAllTickets();                

            if (categoryId != null)
            {
                ticketsQuery = ticketsQuery
                    .Where(t => t.CategoryId == categoryId.Value);
            }

            var tickets = ticketsQuery
                 .To<ListTicketViewModel>();

            return this.Json(tickets.ToDataSourceResult(request));
        }

        [Authorize]
        public ActionResult Add()
        {
            var addTicketModel = new AddTicketViewModel()
            {
                Categories = this.populator.GetCategories()
            };

            return this.View(addTicketModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddTicketViewModel ticket)
        {
            if (ticket != null && this.ModelState.IsValid)
            {
                var dbTicket = AutoMapperConfig.Configuration
               .CreateMapper()
               .Map<Ticket>(ticket);

                dbTicket.Author = this.UserProfile;

                if (ticket.UploadedImage != null)
                {
                    using (var memory = new MemoryStream())
                    {
                        ticket.UploadedImage.InputStream.CopyTo(memory);
                        var content = memory.GetBuffer();

                        dbTicket.Image = new Image()
                        {
                            Content = content,
                            FileExtension = ticket.UploadedImage.FileName.Split(new[] { '.' }).Last()
                        };
                    }
                }

                this.Data.Tickets.Add(dbTicket);
                this.Data.SaveChanges();

                return this.RedirectToAction<TicketsController>(c => c.All(null));
            }

            ticket.Categories = this.populator.GetCategories();

            return this.View(ticket);
        }

        public ActionResult Details(int id)
        {
            var ticket = this.ticketService
                .GetTicketById(id)
               .To<TicketDetailsViewModel>()
               .FirstOrDefault();

            if (ticket == null)
            {
                throw new HttpException(404, "Ticket not found");
            }

            ticket.Comments = this.ticketService
                .GetCommentsByTicketId(id)
                .To<CommentViewModel>()
                .ToList();

            return this.View(ticket);
        }

        public ActionResult Image(int id)
        {
            var image = this.Data
                .Images.GetById(id);

            if (image == null)
            {
                throw new HttpException(404, "Image not found");
            }

            return File(image.Content, "image/" + image.FileExtension);
        }

        public ActionResult GetCategories()
        {
            return Json(this.populator.GetCategories(), JsonRequestBehavior.AllowGet);
        }
    }
}