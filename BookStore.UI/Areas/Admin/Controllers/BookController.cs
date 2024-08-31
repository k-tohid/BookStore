using BookStore.Core.DTO.BookDTO;
using BookStore.Core.DTO.CategoryDTO;
using BookStore.Core.Interfaces.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DiaSymReader;

namespace BookStore.UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class BookController : Controller
	{
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;
        private readonly IBookImageService _bookImageService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(
            IBookService bookService, 
            ICategoryService categoryService, 
            IWebHostEnvironment webHostEnvironment, 
            IBookImageService bookImageService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _bookImageService = bookImageService;
            _webHostEnvironment = webHostEnvironment;

        }

        #region Create Book

        [HttpGet]
		[Route("/[area]/[controller]/create")]
		public async Task<IActionResult> Create()
		{
            IEnumerable<ReadCategoryDTO> categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.categories = categories.Select(temp => new SelectListItem() { Text = temp.Name, Value = temp.Id.ToString() }
   );
            return View();
		}


        [HttpPost]
        [Route("/[area]/[controller]/create")]
        public async Task<IActionResult> Create(CreateBookDTO createBookDTO)
        {

            if (!ModelState.IsValid)
            {
                return View(createBookDTO);
            }

            string? filePath = null;

            if (createBookDTO.ImageFile != null)
            {
                filePath = await _bookImageService.SaveBookImageAsync(createBookDTO.ImageFile, "wwwroot/images/books");
                createBookDTO.ImageUrl = filePath;
            }


            await _bookService.CreateBookAsync(createBookDTO);

            return View();
        }

        #endregion


        #region List Books

        [HttpGet]
        [Route("/[area]/[controller]/list")]
        public async Task<IActionResult> List()
        {
            var books = await _bookService.GetAllBooksAsync();
            return View(books);
            
        }

        #endregion
    }
}
