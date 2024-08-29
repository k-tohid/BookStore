using BookStore.Core.Domain.Entities;
using BookStore.Core.DTO.CategoryDTO;
using BookStore.Core.Interfaces.Repositories;
using BookStore.Core.Interfaces.Services;
using BookStore.Core.Mappings;
using BookStore.Core.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Services
{
	public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
		private readonly ILogger<CategoryService> _logger;

		public CategoryService(ICategoryRepository categoryRepository, ILogger<CategoryService> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

		public async Task<ServiceResult> CreateCategoryAsync(CreateCategoryDTO dto)
		{
            if (dto == null)
                return ServiceResult.Failure("dto can't be null.");

			if (string.IsNullOrEmpty(dto.Name))
				return ServiceResult.Failure("category name can't be null or empty");

			if (!await _categoryRepository.IsCategoryNameUniqueAsync(dto.Name))
                return ServiceResult.Failure("A category with that name is already exists.");

			await _categoryRepository.CreateCategoryAsync(
                new Category { Name = dto.Name, Description = dto.Description }
                );

            var createResult = await _categoryRepository.SaveChangesAsync();

            if (createResult > 0)
                return ServiceResult.Success();

            _logger.LogWarning($"Something went wrong in creating category of {dto}");

            return ServiceResult.Failure("Unknown error occured.");
		}

		public async Task<ReadCategoryDTO?> GetCategoryByIdAsync(int id)
		{
            if (id < 0)
                return null;

            var category = await _categoryRepository.GetCategoryByIdAsync(id);

            if (category == null)
                return null;

            return category.ToCategoryDTO();
		}

		public async Task<ServiceResult> UpdateCategoryAsync(UpdateCategoryDTO dto)
		{
			if (dto == null)
				return ServiceResult.Failure("dto can't be null.");

			if (string.IsNullOrEmpty(dto.Name))
				return ServiceResult.Failure("category name can't be null or empty");


			if (!await _categoryRepository.IsCategoryNameUniqueAsync(dto.Name))
				return ServiceResult.Failure("A category with that name is already exists.");


			var category = await _categoryRepository.GetCategoryByIdAsync(dto.Id);

			if (category == null)
				return ServiceResult.Failure("category with that id does not exist.");

			await _categoryRepository.UpdateCategoryAsync(dto.ToCategory());
			var updateResult = await _categoryRepository.SaveChangesAsync();

			if (updateResult > 0)
				return ServiceResult.Success();

			_logger.LogWarning($"Something went wrong in deleting category with id of {dto.Id}.");

			return ServiceResult.Failure("unknown error.");

		}

		public async Task<ServiceResult> DeleteCategoryAsync(int categoryId)
		{
            if (categoryId < 0)
                return ServiceResult.Failure("Category id should be a positive integer.");

            var category = _categoryRepository.GetCategoryByIdAsync(categoryId);

            if (category == null)
				return ServiceResult.Failure("there is no category with that id.");

			await _categoryRepository.DeleteCategoryAsync(categoryId);

            var deleteResult = await _categoryRepository.SaveChangesAsync();

            if (deleteResult > 0)
                return ServiceResult.Success();

			_logger.LogWarning($"Something went wrong in deleting category with id of {categoryId}.");

			return ServiceResult.Failure("unknown error.");
		}

		public async Task<IEnumerable<ReadCategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return categories.ToCategoryDTO() ?? Enumerable.Empty<ReadCategoryDTO>();
        }
		

	}
}
