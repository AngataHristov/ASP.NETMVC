namespace TicketingSystem.Web.ViewModels.Home
{
    using System;
    using System.Linq;
    using AutoMapper;

    using TicketSystem.Web.Infrastructure.Mapping;
    using Models;

    public class TicketViewModel : IMapFrom<Ticket>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string TicketTitle { get; set; }

        public string CategoryName { get; set; }

        public string AuthorUserName { get; set; }

        public int NumberOfComments { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Ticket, TicketViewModel>()
                .ForMember(t => t.TicketTitle,
                    opt => opt.MapFrom(t => t.Title))
                .ForMember(t => t.CategoryName,
                    opt => opt.MapFrom(t => t.Category.Name))
                .ForMember(t => t.AuthorUserName,
                    opt => opt.MapFrom(t => t.Author.UserName))
                .ForMember(t => t.NumberOfComments,
                    opt => opt.MapFrom(t => t.Comments.Count()))
                .ReverseMap();
        }
    }
}