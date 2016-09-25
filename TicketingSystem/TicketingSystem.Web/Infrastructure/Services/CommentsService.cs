namespace TicketingSystem.Web.Infrastructure.Services
{
    using System;
    using Base;
    using Contracts;
    using Data;
    using ViewModels.Comments;
    using TicketSystem.Web.Infrastructure.Mapping;
    using Models;
    using System.Web;

    public class CommentsService : BaseService, ICommentsService
    {
        public CommentsService(ITicketSystemData data)
            : base(data)
        {

        }

        public CommentViewModel PostComment(PostCommentViewModel comment, User user)
        {
            var dbComment = AutoMapperConfig.Configuration
            .CreateMapper()
            .Map<Comment>(comment);

            dbComment.Author = user;

            var ticket = this.Data.Tickets
                .GetById(comment.TicketId);

            if (ticket == null)
            {
                throw new HttpException(404, "Ticket not found");
            }

            ticket.Comments.Add(dbComment);
            this.Data.SaveChanges();

            var viewModel = AutoMapperConfig.Configuration
                .CreateMapper()
                .Map<CommentViewModel>(dbComment);

            return viewModel;
        }
    }
}