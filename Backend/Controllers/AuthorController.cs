using Backend.Data.Entities;
using Backend.Helpers;
using Backend.Models;
using Backend.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IServiceAsync<Author, AuthorDto> _repository;
        
        public AuthorController(IServiceAsync<Author, AuthorDto> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<AuthorDto>> Get([FromQuery] PagedListQuery<Author> request)
        {
            var result = await _repository.GetAsync(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> Fetch(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            return Ok(result);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Add(AuthorDto model)
        {
            await _repository.AddAsync(model);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update(AuthorDto model)
        {
            await _repository.UpdateAsync(model);
            return Ok();
        }
        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return Ok();
        }
    }
}