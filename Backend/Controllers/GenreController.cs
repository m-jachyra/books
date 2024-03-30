using AutoMapper;
using Backend.Data;
using Backend.Data.Entities;
using Backend.Helpers;
using Backend.Models;
using Backend.Services;
using Backend.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IServiceAsync<Genre, GenreDto> _genreService;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        
        public GenreController(IServiceAsync<Genre, GenreDto> genreService, AppDbContext context, IMapper mapper)
        {
            _genreService = genreService;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<GenreDto>> Get([FromQuery] PagedListQuery<Genre> request)
        {
            var result = await _genreService.GetAsync(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Fetch(int id)
        {
            var result = await _genreService.GetByIdAsync(id);
            return Ok(result);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Add(GenreDto model)
        {
            await _genreService.AddAsync(model);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update(GenreDto model)
        {
            await _genreService.UpdateAsync(model);
            return Ok();
        }
        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            await _genreService.DeleteAsync(id);
            return Ok();
        }
    }
}