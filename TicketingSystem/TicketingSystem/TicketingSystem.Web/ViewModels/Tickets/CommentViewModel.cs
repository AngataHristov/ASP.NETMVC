namespace TicketingSystem.Web.ViewModels.Tickets
{
    using AutoMapper;

    using TicketSystem.Web.Infrastructure.Mapping;

    using Models;
    using System;

    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string AuthorName { get; set; }

        public string Content { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                .ForMember(c => c.AuthorName,
                    opt => opt.MapFrom(c => c.Author.UserName))
                .ReverseMap();
        }
    }
}