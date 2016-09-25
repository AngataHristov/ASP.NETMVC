namespace TicketingSystem.Web.Controllers
{
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.Web.Mvc;
    using TicketSystem.Web.Infrastructure.Mapping;

    using Data;
    using Models;
    using ViewModels.Tickets;
    using System.IO;

    public class TicketsController : BaseController
    {
        public TicketsController(ITicketSystemData data)
            : base(data)
        {
        }

        public ActionResult All()
        {
            var tickets = this.Data.Tickets
                .All()
                .ToList();

            return this.View(tickets);
        }

        [Authorize]
        public ActionResult Add()
        {
            var addTicketModel = new AddTicketViewModel()
            {
                Categories = this.Data.Categories
                    .All()
                    .Select(c => new SelectListItem()
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    })
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

                return this.RedirectToAction<TicketsController>(c => c.All());
            }

            ticket.Categories = this.Data.Categories
                    .All()
                    .Select(c => new SelectListItem()
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    });

            return this.View(ticket);
        }

        public ActionResult Details(int id)
        {
            var ticket = this.Data.Tickets
                .All()
                .Where(t => t.Id == id)
                .To<TicketDetailsViewModel>()
                .FirstOrDefault();

            if (ticket == null)
            {
                throw new HttpException(404, "Ticket not found");
            }

            ticket.Comments = this.Data
                .Comments
                .All()
                .Where(c => c.TicketId == ticket.Id)
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
    }
}