using Backend.Data.Entities;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<ActionResult<ReviewDto>> Get()
        {
            var result = _reviewService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Fetch(int id)
        {
            var result = await _reviewService.GetByIdAsync(id);
            return Ok(result);
        }
        
        [HttpGet("book/{id}")]
        public async Task<ActionResult<ReviewDto>> GetBookReviews(int id, int pageIndex, int pageSize)
        {
            var result = await _reviewService.GetByBookId(id, pageIndex, pageSize);
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<ActionResult> Add(ReviewDto model)
        {
            await _reviewService.AddAsync(model);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update(ReviewDto model)
        {
            await _reviewService.UpdateAsync(model);
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _reviewService.DeleteAsync(id);
            return Ok();
        }
    }
}