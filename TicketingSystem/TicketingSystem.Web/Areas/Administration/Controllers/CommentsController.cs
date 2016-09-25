namespace TicketingSystem.Web.Areas.Administration.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using Microsoft.Web.Mvc;

    using Data;
    using Models;

   // [RoutePrefix("Administration/Comments")]
    public class CommentsController : AdminController
    {
        public CommentsController(ITicketSystemData data)
            : base(data)
        {
        }

        // GET: Administration/Comments
        public ActionResult Index()
        {
            var comments = this.Data.Comments
                .All()
                .Include(c => c.Author)
                .Include(c => c.Ticket);

            return View(comments.ToList());
        }

        // GET: Administration/Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var comment = this.Data
                .Comments
                .GetById(id);

            if (comment == null)
            {
                return HttpNotFound();
            }

            return View(comment);
        }

        // GET: Administration/Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var comment = this.Data
                .Comments
                .GetById(id);

            if (comment == null)
            {
                return HttpNotFound();
            }

            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Comment comment)
        {
            if (ModelState.IsValid)
            {
                this.Data.Context.Entry(comment).State = EntityState.Modified;
                this.Data.SaveChanges();

                return this.RedirectToAction<CommentsController>(c => c.Index());
            }

            return View(comment);
        }

        // GET: Administration/Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var comment = this.Data
                .Comments
                .GetById(id);

            if (comment == null)
            {
                return HttpNotFound();
            }

            return View(comment);
        }

        // POST: Administration/Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            this.Data.Comments.Delete(id);
            this.Data.SaveChanges();

            return this.RedirectToAction<CommentsController>(c => c.Index());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Data.Context.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
