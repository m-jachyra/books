using Backend.Data.Entities;
using Backend.Helpers;
using Backend.Models;
using Backend.Models.Author;
using Backend.Services;
using Backend.Services.Base;
using Backend.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorService _authorService;
        private readonly IStorageService _storageService;
        
        public AuthorController(AuthorService authorService, IStorageService storageService)
        {
            _authorService = authorService;
            _storageService = storageService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<AuthorListDto>> Get([FromQuery] PagedListQuery<Author> request)
        {
            var result = await _authorService.GetMappedPagedListAsync(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthorDetailsDto>> Fetch(int id)
        {
            var result = await _authorService.GetByIdAsync(id);
            return Ok(result);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Add(AuthorUpdateDto model)
        {
            await _authorService.AddAsync(model);
            return Ok();
        }
        
        [HttpPost("image")]
        [Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<ActionResult> UploadImage(PictureDto model)
        {
            await _authorService.UpdatePicturePath(model);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update(AuthorUpdateDto model)
        {
            await _authorService.UpdateAsync(model);
            return Ok();
        }
        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            await _authorService.DeleteAsync(id);
            return Ok();
        }
    }
}