using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Application.Abstraction.Dtos.Categorires;
using BlogSystem.Shared._ٌApi_Response;

namespace BlogSystem.Application.Abstraction.Contracts.Services.Categories
{
    public interface ICategoryService
    {
        Task<ApiResponse<IEnumerable<CategoryDto>>> GetAllAsync();
        Task<ApiResponse<CategoryDto>> CreateAsync(CategoryCreateDto dto);
        Task<ApiResponse<CategoryDto>> UpdateAsync(int id, CategoryCreateDto dto);
        Task<ApiResponse<bool>> DeleteAsync(int id);
    }
}
