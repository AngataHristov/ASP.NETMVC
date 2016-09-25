namespace MvcTemplate.Web.ViewModels.Home
{
    using System;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using Services.Web;

    public class JokeViewModel : IMapFrom<Joke>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string Category { get; set; }

        public string Url
        {
            get
            {
                IIdentifierProvider provider = new IdentifierProvider();

                return $"/Joke/{provider.EncodeId(this.Id)}";
            }
        }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Joke, JokeViewModel>()
                .ForMember(
                    j => j.Category,
                    opt => opt.MapFrom(j => j.Category.Name));
        }
    }
}