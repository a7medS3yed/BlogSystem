using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Application.Abstraction.Dtos.Tags;
using BlogSystem.Shared._ٌApi_Response;

namespace BlogSystem.Application.Abstraction.Contracts.Services.Tags
{
    public interface ITagService
    {
        Task<ApiResponse<IEnumerable<TagDto>>> GetAllAsync();
        Task<ApiResponse<TagDto>> CreateAsync(TagCreateDto dto);
        Task<ApiResponse<TagDto>> UpdateAsync(int id, TagCreateDto dto);
        Task<ApiResponse<bool>> DeleteAsync(int id);
    }
}
