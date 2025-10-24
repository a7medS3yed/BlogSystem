using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BlogSystem.Application.Abstraction.Contracts.Services.Tags;
using BlogSystem.Application.Abstraction.Contracts.UnitOfWork;
using BlogSystem.Application.Abstraction.Dtos.Tags;
using BlogSystem.Domain.Entities.Post;
using BlogSystem.Shared._ٌApi_Response;

namespace BlogSystem.Application.Services.Tags
{
    internal class TagService(IUnitOfWork unitOfWork, IMapper mapper) : ITagService
    {
        public async Task<ApiResponse<IEnumerable<TagDto>>> GetAllAsync()
        {
            var tags = await unitOfWork.GenaricRepository<Tag, int>().GetAsync();
            var result = mapper.Map<IEnumerable<TagDto>>(tags);
            return ApiResponse<IEnumerable<TagDto>>.SuccessResponse(result);
        }

        public async Task<ApiResponse<TagDto>> CreateAsync(TagCreateDto dto)
        {
            var tag = mapper.Map<Tag>(dto);
            await unitOfWork.GenaricRepository<Tag, int>().AddAsync(tag);
            await unitOfWork.CompleteAsync();

            var result = mapper.Map<TagDto>(tag);
            return ApiResponse<TagDto>.SuccessResponse(result, "Tag created successfully");
        }

        public async Task<ApiResponse<TagDto>> UpdateAsync(int id, TagCreateDto dto)
        {
            var tagRepo = unitOfWork.GenaricRepository<Tag, int>(); // Removed 'await' as the method is not asynchronous
            var tag = await tagRepo.GetByIdAsync(id); // Correctly awaiting the asynchronous method
            if (tag == null)
            {
                return ApiResponse<TagDto>.FailResponse("Tag not found");
            }
            mapper.Map(dto, tag);
            tagRepo.Update(tag);
            await unitOfWork.CompleteAsync();
            var result = mapper.Map<TagDto>(tag);
            return ApiResponse<TagDto>.SuccessResponse(result, "Tag updated successfully");
        }

        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
           var tagRepo = unitOfWork.GenaricRepository<Tag, int>(); // Removed 'await' as the method is not asynchronous
            var tag = await tagRepo.GetByIdAsync(id); // Correctly awaiting the asynchronous method
            if (tag == null)
            {
                return ApiResponse<bool>.FailResponse("Tag not found");
            }
            tagRepo.Delete(tag.Id); // Pass the Id instead of the entity
            await unitOfWork.CompleteAsync();
            return ApiResponse<bool>.SuccessResponse(true, "Tag deleted successfully");
        }
    }

}
