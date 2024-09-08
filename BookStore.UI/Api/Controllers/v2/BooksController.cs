using Asp.Versioning;
using BookStore.Core.DTO.BookDTO;
using BookStore.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.UI.Api.Controllers.v2
{
    //[Route("api/v{version:ApiVersion}/[controller]")]
    [ApiVersion("2.0")]
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
        [Produces("application/json")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();

            return Ok(books);
        }
    }
}
