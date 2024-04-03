using System.Security.Claims;
using Backend.Auth;
using Backend.Data.Entities;
using Backend.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        public record LoginDto(string Email, string Password);
        
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] LoginDto request)
        {
            var result = await _authService.LoginAsync(request.Email, request.Password);

            if (result == null)
                return BadRequest();
            
            return Ok(result);
        }
        
        [HttpPost("logout")]
        [AllowAnonymous]
        public async Task<ActionResult> Logout()
        {
            await _authService.LogoutAsync(User);
            
            return Ok();
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] LoginDto request)
        {
            var result = await _authService.RegisterAsync(request.Email, request.Password);

            return result ? Ok() : BadRequest();
        }
        
        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<ActionResult> RefreshToken([FromBody] TokenDto token)
        {
            var result = await _authService.GenerateAccessTokenFromRefreshToken(User, token.RefreshToken);

            if (result == null)
                return BadRequest();
            
            return Ok(result);
        }
    }
}