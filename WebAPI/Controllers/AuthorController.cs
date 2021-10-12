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
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
            
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAuthorRequestModel model)
        {
            var response = await _authorService.AddAuthor(model);
            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor([FromRoute] int id)
        {
            var response = await _authorService.GetAuthor(id);
            return Ok(response);
        }
    }
}
