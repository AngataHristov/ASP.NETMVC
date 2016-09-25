namespace TicketingSystem.Web.ViewModels.Tickets
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using TicketSystem.Web.Infrastructure.Mapping;

    using Models;
    using Models.Enums;
    using Comments;

    public class TicketDetailsViewModel : IMapFrom<Ticket>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public PriorityType Priority { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string AuthorName { get; set; }

        public string CategoryName { get; set; }

        public int? ImageId { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Ticket, TicketDetailsViewModel>()
                .ForMember(t => t.AuthorName,
                    opt => opt.MapFrom(t => t.Author.UserName))
                .ForMember(t => t.CategoryName,
                    opt => opt.MapFrom(t => t.Category.Name))
                .ReverseMap();
        }
    }
}