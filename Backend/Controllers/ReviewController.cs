using Backend.Data.Entities;
using Backend.Helpers;
using Backend.Models;
using Backend.Models.Review;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Backend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService _reviewService;
        
        public ReviewController(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        
        [HttpGet("book/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ReviewListDto>> GetBookReviews([FromRoute] int id, [FromQuery] PagedListQuery<Review> request)
        {
            return Ok(await _reviewService.GetByBookId(id, request, User.Id()));
        }
        
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ReviewListDto>> GetReview([FromRoute] int id, [FromQuery] PagedListQuery<Review> request)
        {
            return Ok(await _reviewService.GetByIdAsync(id));
        }
        
        [HttpGet("top")]
        [AllowAnonymous]
        public async Task<ActionResult<ReviewListDto>> GetTopReviews()
        {
            return Ok(await _reviewService.GetTopReviews(User.Id()));
        }
        
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Add(ReviewUpdateDto model)
        {
            await _reviewService.AddAsync(model, User.Id().Value);
            return Ok();
        }
        
        [HttpPost("{id}/plus")]
        [Authorize]
        public async Task<ActionResult> AddPlus([FromRoute] int id)
        {
            await _reviewService.AddPlus(User.Id().Value, id);
            return Ok();
        }
        
        [HttpDelete("{id}/plus")]
        [Authorize]
        public async Task<ActionResult> RemovePlus([FromRoute] int id)
        {
            await _reviewService.RemovePlus(User.Id().Value, id);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update(ReviewUpdateDto model)
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