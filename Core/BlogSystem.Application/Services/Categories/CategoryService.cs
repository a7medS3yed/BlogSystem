using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BlogSystem.Application.Abstraction.Contracts.Services.Categories;
using BlogSystem.Application.Abstraction.Contracts.UnitOfWork;
using BlogSystem.Application.Abstraction.Dtos.Categorires;
using BlogSystem.Domain.Entities.Post;
using BlogSystem.Shared._ٌApi_Response;

namespace BlogSystem.Application.Services.Categories
{
    internal class CategoryService(IUnitOfWork unitOfWork, IMapper mapper) : ICategoryService
    {
        public async Task<ApiResponse<IEnumerable<CategoryDto>>> GetAllAsync()
        {
            var categories = await unitOfWork.GenaricRepository<Category, int>().GetAsync();
            var result = mapper.Map<IEnumerable<CategoryDto>>(categories);
            return ApiResponse<IEnumerable<CategoryDto>>.SuccessResponse(result);
        }

        public async Task<ApiResponse<CategoryDto>> CreateAsync(CategoryCreateDto dto)
        {
            var category = mapper.Map<Category>(dto);
            await unitOfWork.GenaricRepository<Category, int>().AddAsync(category);
            await unitOfWork.CompleteAsync();

            var result = mapper.Map<CategoryDto>(category);
            return ApiResponse<CategoryDto>.SuccessResponse(result, "Category created successfully");
        }

        public async Task<ApiResponse<CategoryDto>> UpdateAsync(int id, CategoryCreateDto dto)
        {
            
            var category = await unitOfWork.GenaricRepository<Category, int>().GetByIdAsync(id);
            if (category == null)
                return ApiResponse<CategoryDto>.FailResponse("Category not found");

            
            mapper.Map(dto, category);

            
            unitOfWork.GenaricRepository<Category, int>().Update(category);
            await unitOfWork.CompleteAsync();

            
            var result = mapper.Map<CategoryDto>(category);
            return ApiResponse<CategoryDto>.SuccessResponse(result, "Category updated successfully");
        }


        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            var category = await unitOfWork.GenaricRepository<Category, int>().GetByIdAsync(id);
            if (category == null)
            {
                return ApiResponse<bool>.FailResponse("Category not found");
            }

            unitOfWork.GenaricRepository<Category, int>().Delete(category.Id);
            await unitOfWork.CompleteAsync();

            return ApiResponse<bool>.SuccessResponse(true, "Category deleted successfully");
        }

    }

}
