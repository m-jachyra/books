﻿using Backend.Data.Entities;
using Backend.Helpers;
using Backend.Models;
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
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;
        private readonly StorageService _storageService;
        
        public BookController(BookService bookService, StorageService storageService)
        {
            _bookService = bookService;
            _storageService = storageService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<BookDto>> Get([FromQuery] PagedListQuery<Book> request)
        {
            var result = await _bookService.GetAsync(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> Fetch(int id)
        {
            var result = await _bookService.GetByIdAsync(id);
            return Ok(result);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Add(BookDto model)
        {
            await _bookService.AddAsync(model);
            return Ok();
        }
        
        [HttpPost("image")]
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<ActionResult> UploadImage(PictureDto model)
        {
            var fileName = $"picture_{model.Id}";
            await _storageService.UploadFileAsync(model.File, fileName);
            await _bookService.UpdatePicturePath(model);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update(BookDto model)
        {
            await _bookService.UpdateAsync(model);
            return Ok();
        }
        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            await _bookService.DeleteAsync(id);
            return Ok();
        }
    }
}