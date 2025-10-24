using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BlogSystem.Application.Abstraction.Dtos.Post;
using BlogSystem.Domain.Entities.Post;

namespace BlogSystem.Application.Mapper
{
    public class BlogPostProfile : Profile
    {
        public BlogPostProfile()
        {
            CreateMap<PostCreateDto, BlogPost>();

            CreateMap<PostUpdateDto, BlogPost>();

            CreateMap<BlogPost, PostDto>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.UserName))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.BlogPostTags.Select(bt => bt.Tag.Name)));
        }
    }
}
