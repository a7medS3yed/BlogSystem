using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BlogSystem.Application.Abstraction.Dtos.Categorires;
using BlogSystem.Domain.Entities.Post;

namespace BlogSystem.Application.Mapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryCreateDto, Category>();
        }
    }
}
