namespace TicketingSystem.Web.ViewModels.Tickets
{
    using System;

    using AutoMapper;

    using Models;
    using TicketSystem.Web.Infrastructure.Mapping;

    public class ListTicketViewModel : IMapFrom<Ticket>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string TicketTitle { get; set; }

        public string CategoryName { get; set; }

        public string AuthorName { get; set; }

        public string Priority { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Ticket, ListTicketViewModel>()
                .ForMember(t => t.TicketTitle,
                    opt => opt.MapFrom(t => t.Title))
                .ForMember(t => t.CategoryName,
                    opt => opt.MapFrom(t => t.Category.Name))
                .ForMember(t => t.AuthorName,
                    opt => opt.MapFrom(t => t.Author.UserName))
                .ForMember(t => t.Priority,
                    opt => opt.MapFrom(t => t.Priority.ToString()))
                .ReverseMap();
        }
    }
}