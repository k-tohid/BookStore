using BookStore.Core.DTO.BookDTO;
using BookStore.Core.DTO.CategoryDTO;
using BookStore.Core.Interfaces.Services;
using BookStore.Core.Mappings;
using Humanizer;
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
            ViewBag.categories = categories.Select(temp => new SelectListItem() { Text = temp.Name, Value = temp.Id.ToString() });
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


        #region Update Book

        [HttpGet]
		[Route("/[area]/[controller]/update")]
		public async Task<IActionResult> Update(int id)
        {
            if (id < 1)
            {
                ModelState.AddModelError(string.Empty, "Invalid Book id.");
                return View();
            }

            var book = await _bookService.GetBookByIdAsync(id);
            
            if (book == null)
            {
                return RedirectToAction("List", "Book");
            }

            IEnumerable<ReadCategoryDTO> categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.categories = categories.Select(temp => new SelectListItem() { Text = temp.Name, Value = temp.Id.ToString() });

            return View(book.ToUpdateBookDTO());
            
        }


        [HttpPost]
        [Route("/[area]/[controller]/update")]
        public async Task<IActionResult> Update(UpdateBookDTO dto)
        {

            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            string? filePath = null;

            if (dto.ImageFile != null)
            {
                if (dto.ImageUrl != null)
                {
                    _bookImageService.DeleteBookImage($"wwwroot/images/books/{dto.ImageUrl}");
                    dto.ImageUrl = null;
                }

                filePath = await _bookImageService.SaveBookImageAsync(dto.ImageFile, "wwwroot/images/books");
                dto.ImageUrl = filePath;
            }


            var updateResult = await _bookService.UpdateBookAsync(dto);


            if (!updateResult.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, updateResult.ErrorMessage);
                return View(dto);
            }

            return RedirectToAction("list", "Book");

        }

        #endregion


        #region Delete Book

        [Route("/[area]/[controller]/delete")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
            {
                return RedirectToAction("list", "Book");
            }

            return View(book);
        }

        [Route("/[area]/[controller]/confirmDelete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
            {
                return RedirectToAction("list", "Book");
            }

            _bookImageService.DeleteBookImage($"wwwroot/images/books/{book.ImageUrl}");
            var deleteResult = await _bookService.DeleteBookAsync(id);

            if (!deleteResult.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, deleteResult.ErrorMessage);
                return View(book);
            }

            return RedirectToAction("list", "Book");
        }

        #endregion
    }
}
