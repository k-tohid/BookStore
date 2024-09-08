using Asp.Versioning;
using BookStore.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.UI.Api.Controllers.v1
{
    //[Route("api/v{version:ApiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// You can get all the books
        /// </summary>
        /// <returns>returns a list of books</returns>
        [HttpGet]
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();

            return Ok(books);
        }
    }
}
