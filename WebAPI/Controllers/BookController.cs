using Domain.Enums;
using Domain.Interfaces.Services;
using Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [Route("AddBook")]
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] CreateBookRequestModel model)
        {
            var response = await _bookService.AddBook(model);
            return Ok(response);
        }

        /*[HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, UpdateBookRequestModel model)
        {
            var response = await _bookService.UpdateBook(id, model);
            return Ok(response);
        }*/
        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            var response = await _bookService.DeleteBook(id);
            return Ok(response);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetBookById([FromRoute] int id)
        {
            var response = await _bookService.GetBook(id);
            return Ok(response);
        }

        [Route("GetAllBooks")]
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var response = await _bookService.GetBooks();
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetBookByTitle([FromBody] string title)
        {
            var response = await _bookService.GetBookByTitle(title);
            return Ok(response);
        }

        /*[HttpGet]
        public async Task<IActionResult> GetBookByPublicationDate([FromBody] DateTime publicationDate)
        {
            var response = await _bookService.GetBooksByPublicationDate(publicationDate);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetBookByPublisher([FromBody] string publisher)
        {
            var response = await _bookService.GetBooksByPublisher(publisher);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooksByCategory([FromRoute] int categoryId)
        {
            var response = await _bookService.GetBooksByCategory(categoryId);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooksByAuthor([FromRoute] int authorId)
        {
            var response = await _bookService.GetBooksByAuthor(authorId);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetBookByAvailabilityStatus([FromBody] BookAvailabilityStatus availabilityStatus)
        {
            var response = await _bookService.GetBooksByAvailabilityStatus(availabilityStatus);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetBooksByAccessibilityStatus([FromBody] BookAccessibilityStatus accessibilityStatus)
        {
            var response = await _bookService.GetBooksByAccessibilityStatus(accessibilityStatus);
            return Ok(response);
        }*/
    }
}
