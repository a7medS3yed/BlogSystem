using AutoMapper;
using BlogSystem.Application.Abstraction.Dtos.Tags;
using BlogSystem.Domain.Entities.Post;

namespace BlogSystem.Application.Mapper
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, TagDto>();
            CreateMap<TagCreateDto, Tag>();
        }
    }
}
