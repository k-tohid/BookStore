using BookStore.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Interfaces.Repositories
{
    public interface IBookRepository
    {
		Task<int> SaveChangesAsync();
		Task<Book?> GetBookByIdAsync(int id);
        Task CreateBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
		Task<IEnumerable<Book>> GetAllBooksAsync();
		Task<IEnumerable<Book>> GetBooksByCategoryAsync(int categoryId);
		Task<bool> IsBookTitleUniqueAsync(string bookTitle);
		Task<IEnumerable<Book>> SearchBooksAsync(string title, string author);
    }
}
