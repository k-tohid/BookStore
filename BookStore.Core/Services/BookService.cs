using BookStore.Core.Domain.Entities;
using BookStore.Core.DTO.BookDTO;
using BookStore.Core.Interfaces.Repositories;
using BookStore.Core.Interfaces.Services;
using BookStore.Core.Mappings;
using BookStore.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Services
{
	public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
		private readonly ILogger<BookService> _logger;

		public BookService(IBookRepository bookRepository, ILogger<BookService> logger)
        {
            _bookRepository = bookRepository;
			_logger = logger;
        }

		public async Task<IEnumerable<ReadBookDTO>> GetAllBooksAsync()
		{
			var books = await _bookRepository.GetAllBooksAsync();
			return books.ToReadBookDTO();
		}

		public async Task<ServiceResult> CreateBookAsync(CreateBookDTO dto)
		{
			if (dto == null)
				return ServiceResult.Failure("dto can't be null.");

			if (string.IsNullOrEmpty(dto.Title) || string.IsNullOrEmpty(dto.Author))
				return ServiceResult.Failure("Book title and author name can't be null or empty");

			if (!await _bookRepository.IsBookTitleUniqueAsync(dto.Title))
				return ServiceResult.Failure("A book with that title is already exists.");

			await _bookRepository.CreateBookAsync(
				new Book
				{
					Title = dto.Title,
					Author = dto.Author,
					Summary = dto.Summary,
					ImageUrl = dto.ImageUrl,
					Price = dto.Price,
					Stock = dto.Stock,
					CategoryId = dto.CategoryId
				}
				);

			var createResult = await _bookRepository.SaveChangesAsync();

			if (createResult > 0)
				return ServiceResult.Success();

			_logger.LogWarning($"Something went wrong in creating book of {dto}");

			return ServiceResult.Failure("Unknown error occured.");
		}

		public Task<ServiceResult> DeleteBookAsync(int bookId)
		{
			throw new NotImplementedException();
		}


		public Task<ReadBookDTO?> GetBookByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<ServiceResult> UpdateBookAsync(UpdateBookDTO dto)
		{
			throw new NotImplementedException();
		}
	}
}
