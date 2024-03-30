using Backend.Data.Entities;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        
        [HttpGet("book/{id}")]
        [AllowAnonymous]
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            await _reviewService.DeleteAsync(id);
            return Ok();
        }
    }
}