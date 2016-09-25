namespace TicketingSystem.Web.ViewModels.Comments
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using TicketSystem.Web.Infrastructure.Mapping;

    using Models;
    using AutoMapper;

    public class PostCommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public PostCommentViewModel()
        {
        }

        public PostCommentViewModel(int id)
        {
            this.TicketId = id;
        }

        public int TicketId { get; set; }

        [MinLength(50)]
        [MaxLength(1000)]
        [Required]
        [UIHint("MultiLineText")]
        public string Content { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Comment, PostCommentViewModel>()
                .ForMember(m => m.Content,
                    opt => opt.MapFrom(c => c.Content))
                .ReverseMap();
        }
    }
}