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

		public async Task<ServiceResult> DeleteBookAsync(int bookId)
		{
            if (bookId < 0)
                return ServiceResult.Failure("Book id should be a positive integer.");

            var book = _bookRepository.GetBookByIdAsync(bookId);

            if (book == null)
                return ServiceResult.Failure("there is no book with that id.");

            await _bookRepository.DeleteBookAsync(bookId);

            var deleteResult = await _bookRepository.SaveChangesAsync();

            if (deleteResult > 0)
                return ServiceResult.Success();

            _logger.LogWarning($"Something went wrong in deleting book with id of {bookId}.");

            return ServiceResult.Failure("unknown error.");
        }


		public async Task<ReadBookDTO?> GetBookByIdAsync(int id)
		{
			var book = await _bookRepository.GetBookByIdAsync(id);
			return book?.ToReadBookDTO();
		}

		public async Task<ServiceResult> UpdateBookAsync(UpdateBookDTO dto)
		{
            if (dto == null)
                return ServiceResult.Failure("dto can't be null.");

            if (string.IsNullOrEmpty(dto.Title))
                return ServiceResult.Failure("book title can't be null or empty");


            if (!await _bookRepository.IsBookTitleUniqueAsync(dto.Title))
                return ServiceResult.Failure("A Book with that title is already exists.");


            var book = await _bookRepository.GetBookByIdAsync(dto.Id);

            if (book == null)
                return ServiceResult.Failure("Book with that id does not exist.");

            await _bookRepository.UpdateBookAsync(dto.ToBook());
            var updateResult = await _bookRepository.SaveChangesAsync();

            if (updateResult > 0)
                return ServiceResult.Success();

            _logger.LogWarning($"Something went wrong in updating book with id of {dto.Id}.");

            return ServiceResult.Failure("unknown error.");
        }
	}
}
