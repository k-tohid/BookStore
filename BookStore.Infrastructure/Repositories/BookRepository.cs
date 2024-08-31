using BookStore.Core.Domain.Entities;
using BookStore.Core.Interfaces.Repositories;
using BookStore.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<BookRepository> _logger;

        public BookRepository(ApplicationDbContext db, ILogger<BookRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

		public async Task<int> SaveChangesAsync()
		{
			return await _db.SaveChangesAsync();
		}

		public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _db.Books.FindAsync(id);
        }

        public async Task CreateBookAsync(Book book)
        {
            await _db.Books.AddAsync(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _db.Books.FindAsync(id);

            if (book == null)
                throw new ArgumentException("Book not found by that id.");

            _db.Books.Remove(book);
        }

		public async Task UpdateBookAsync(Book book)
		{
			var exsistingBook = await _db.Categories.FindAsync(book.Id);
			if (exsistingBook == null)
			{
				throw new InvalidOperationException("Category not found.");
			}

			// Update the existing entity's properties with the new values
			_db.Entry(exsistingBook).CurrentValues.SetValues(book);
		}

		public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _db.Books.Include("Category").ToListAsync();
        }

        public Task<IEnumerable<Book>> GetBooksByCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> SearchBooksAsync(string title, string author)
        {
            throw new NotImplementedException();
        }

		public async Task<bool> IsBookTitleUniqueAsync(string bookTitle)
		{
			return !await _db.Books.AnyAsync(c => c.Title == bookTitle);
		}
	}
}
