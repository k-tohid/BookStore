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

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _db.Books.FindAsync(id);
        }

        public async Task<bool> AddBookAsync(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            await _db.Books.AddAsync(book);
            var result = await _db.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _db.Books.FindAsync(id);

            if (book == null)
                throw new ArgumentException("Book not found by that id.");

            _db.Books.Remove(book);
            var result = await _db.SaveChangesAsync();

            return result > 0;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _db.Books.Include("Category").ToListAsync();
        }

        public async Task<bool> UpdateBookAsync(Book book)
        {
            _db.Books.Update(book);
            var result = await _db.SaveChangesAsync();

            return result > 0;
        }

        public Task<IEnumerable<Book>> GetBooksByCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> SearchBooksAsync(string title, string author)
        {
            throw new NotImplementedException();
        }

        
    }
}
