﻿using BookStore.Core.Domain.Entities;
using BookStore.Core.Interfaces.Repositories;
using BookStore.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Repositories
{
	public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> IsCategoryNameUniqueAsync(string categoryName)
        {
            return !await _db.Categories.AnyAsync(c => c.Name == categoryName);
        }

        public async Task CreateCategoryAsync(Category category)
        {
            await _db.Categories.AddAsync(category);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
			var existingCategory = await _db.Categories.FindAsync(category.Id);
			if (existingCategory == null)
			{
				throw new InvalidOperationException("Category not found.");
			}

			// Update the existing entity's properties with the new values
			_db.Entry(existingCategory).CurrentValues.SetValues(category);
		}

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _db.Categories.FindAsync(id);

            if (category == null)
                throw new ArgumentException("Category not found by that id.");

            _db.Categories.Remove(category);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _db.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _db.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Category>> SearchCategoryAsync(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                return await _db.Categories.ToListAsync();
            }

            // Perform a case-insensitive search
            var categories = await _db.Categories.Where(c => EF.Functions.Like(c.Name, $"%{categoryName}%")).ToListAsync();
            return categories;
        }

		public async Task<int> SaveChangesAsync()
		{
            return await _db.SaveChangesAsync();
		}
	}
}
