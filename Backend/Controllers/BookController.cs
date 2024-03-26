using Backend.Data.Entities;
using Backend.Models;
using Backend.Services;
using Backend.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IServiceAsync<Book, BookDto> _repository;
        
        public BookController(IServiceAsync<Book, BookDto> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<BookDto>> Get(CancellationToken cancellationToken)
        {
            var result = _repository.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Fetch(int id, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByIdAsync(id);
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<ActionResult> Add(BookDto model, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(model);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update(BookDto model, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(model);
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(id);
            return Ok();
        }
    }
}