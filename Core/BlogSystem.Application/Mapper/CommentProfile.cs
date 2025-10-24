using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BlogSystem.Application.Abstraction.Dtos.Comment;
using BlogSystem.Domain.Entities.Comments;

namespace BlogSystem.Application.Mapper
{
    internal class CommentProfile : Profile
    {
        public CommentProfile()
        {
            // Map Comment → CommentDto
            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.UserName))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn));

            // Map CommentCreateDto → Comment
            CreateMap<CommentCreateDto, Comment>();
        }
    }
}
