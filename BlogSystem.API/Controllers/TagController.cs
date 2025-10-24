using BlogSystem.Application.Abstraction.Contracts.ServiceManager;
using BlogSystem.Application.Abstraction.Dtos.Tags;
using BlogSystem.Shared._ٌApi_Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController(IServiceMangar serviceMangar) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<TagDto>>>> GetAll()
        {
            var response = await serviceMangar.TagService.GetAllAsync();
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<TagDto>>> Create([FromBody] TagCreateDto dto)
        {
            var response = await serviceMangar.TagService.CreateAsync(dto);
            return Ok(response);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        public async Task<ActionResult<ApiResponse<TagDto>>> Update([FromRoute] int id, [FromBody] TagCreateDto dto)
        {
            var response = await serviceMangar.TagService.UpdateAsync(id, dto);
            return Ok(response);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete([FromRoute] int id)
        {
            var response = await serviceMangar.TagService.DeleteAsync(id);
            return Ok(response);
        }
    }

}
