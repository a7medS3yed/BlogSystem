using BlogSystem.Application.Abstraction.Contracts.ServiceManager;
using BlogSystem.Application.Abstraction.Dtos.Categorires;
using BlogSystem.Shared._ٌApi_Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(IServiceMangar serviceMangar) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<CategoryDto>>>> GetAll()
        {
            var response = await serviceMangar.categoryService.GetAllAsync();
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<CategoryDto>>> Create([FromBody] CategoryCreateDto dto)
        {
            var response = await serviceMangar.categoryService.CreateAsync(dto);
            return Ok(response);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("{id:int}")]
        // PUT api/category/5
        public async Task<ActionResult<ApiResponse<CategoryDto>>> Update(int id, [FromBody] CategoryCreateDto dto)
        {
            var response = await serviceMangar.categoryService.UpdateAsync(id, dto);
            return Ok(response);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("{id:int}")]
        // DELETE api/category/5
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var response = await serviceMangar.categoryService.DeleteAsync(id);
            return Ok(response);
        }
    }

}
