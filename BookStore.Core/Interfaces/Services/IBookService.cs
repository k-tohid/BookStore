
using BookStore.Core.DTO.BookDTO;
using BookStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Interfaces.Services
{
    public interface IBookService
    {
		Task<IEnumerable<ReadBookDTO>> GetAllBooksAsync();
		Task<ServiceResult> CreateBookAsync(CreateBookDTO dto);
		Task<ReadBookDTO?> GetBookByIdAsync(int id);
		Task<ServiceResult> UpdateBookAsync(UpdateBookDTO dto);
		Task<ServiceResult> DeleteBookAsync(int bookId);
	}
}
