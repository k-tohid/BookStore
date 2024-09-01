using BookStore.Core.DTO.CategoryDTO;
using BookStore.Core.Interfaces.Services;
using BookStore.Core.Mappings;
using BookStore.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


		#region List Categories

		[HttpGet]
        [Route("/[area]/[controller]/list")]
        public async Task<IActionResult> List()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories);
        }

		#endregion


		#region Create Category

		[HttpGet]
		[Route("/[area]/[controller]/create")]
		public IActionResult Create()
        {  
            return View();
        }


		[HttpPost]
		[Route("/[area]/[controller]/create")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateCategoryDTO dto)
		{
			if (dto == null)
			{
				ModelState.AddModelError(string.Empty, "Invalid create category attempt.");
				return View();
			}

			if (!ModelState.IsValid)
			{
				return View(dto);
			}

			ServiceResult serviceResult = await _categoryService.CreateCategoryAsync(dto);

            if (!serviceResult.IsSuccess)
            {
				ModelState.AddModelError(string.Empty, serviceResult.ErrorMessage);
                return View(dto);
            }

			return RedirectToAction("list", "Category");

		}

		#endregion


		#region Delete Category

		[Route("/[area]/[controller]/delete")]
		[HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if ( category == null)
            {
				return RedirectToAction("list", "Category");
            }

            return View(category);
        }

		[Route("/[area]/[controller]/confirmDelete")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ConfirmDelete(int id)
		{
			var category = await _categoryService.GetCategoryByIdAsync(id);

			if (category == null)
			{
				return RedirectToAction("list", "Category");
			}


			var deleteResult = await _categoryService.DeleteCategoryAsync(id);

			if (!deleteResult.IsSuccess)
			{
				ModelState.AddModelError(string.Empty, deleteResult.ErrorMessage);
				return View(category);
			}

			return RedirectToAction("list", "Category");
		}

		#endregion


		#region Update Category

		[Route("/[area]/[controller]/update")]
		[HttpGet]
		public async Task<IActionResult> Update(int id)
		{
            if (id < 1)
            {
                ModelState.AddModelError(string.Empty, "Invalid Category id.");
                return View();
            }

            var category = await _categoryService.GetCategoryByIdAsync(id);

			if (category == null)
			{
				return RedirectToAction("list", "Category");
			}


			return View(category.ToUpdateCategoryDTO());
		}

		[Route("/[area]/[controller]/update")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(UpdateCategoryDTO updateCategoryDTO)
		{
			
			var updateResult = await _categoryService.UpdateCategoryAsync(updateCategoryDTO);

			if (!updateResult.IsSuccess)
			{
				ModelState.AddModelError(string.Empty, updateResult.ErrorMessage);
				return View(updateCategoryDTO);
			}

			return RedirectToAction("list", "Category");
		}

		#endregion
	}
}
