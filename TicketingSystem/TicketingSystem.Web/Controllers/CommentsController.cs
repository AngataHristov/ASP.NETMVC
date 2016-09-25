namespace TicketingSystem.Web.Controllers
{
    using System.Web;
    using System.Web.Mvc;

    using Data;
    using Infrastructure.Services.Contracts;
    using ViewModels.Comments;

    public class CommentsController : BaseController
    {
         private readonly ICommentsService commentsService;

        public CommentsController(ITicketSystemData data, ICommentsService commentsService)
            : base(data)
        {
             this.commentsService = commentsService;
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
       // [Route("Comments/")]
        public ActionResult PostComment(PostCommentViewModel comment)
        {
            if (comment != null && this.ModelState.IsValid)
            {
                var viewModel = this.commentsService.PostComment(comment, this.UserProfile);

                return this.PartialView("_CommentPartial", viewModel);
            }

            throw new HttpException(400, "Invalid comment");
        }
    }
}