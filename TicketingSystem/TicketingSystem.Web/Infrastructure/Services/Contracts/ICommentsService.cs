namespace TicketingSystem.Web.Infrastructure.Services.Contracts
{
    using Models;
    using ViewModels.Comments;

    public interface ICommentsService
    {
        CommentViewModel PostComment(PostCommentViewModel comment, User user);
    }
}