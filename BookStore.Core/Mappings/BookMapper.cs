using BookStore.Core.Domain.Entities;
using BookStore.Core.DTO.BookDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Mappings
{
    public static class BookMapper
    {
        public static ReadBookDTO ToReadBookDTO(this Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            return new ReadBookDTO
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Summary = book.Summary,
                Price = book.Price,
                Stock = book.Stock,
                ImageUrl = book.ImageUrl,
                CategoryId = book.CategoryId,
            };
        }

        public static IEnumerable<ReadBookDTO> ToReadBookDTO(this IEnumerable<Book> books)
        {
            return books.Select(book => ToReadBookDTO(book));
        }


        public static UpdateBookDTO ToUpdateBookDTO(this ReadBookDTO book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            return new UpdateBookDTO
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Summary = book.Summary,
                Price = book.Price,
                Stock = book.Stock,
                ImageUrl = book.ImageUrl,
                CategoryId = book.CategoryId,
            };
        }


        public static Book ToBook(this UpdateBookDTO book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            return new Book
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Summary = book.Summary,
                Price = book.Price,
                Stock = book.Stock,
                ImageUrl = book.ImageUrl,
                CategoryId = book.CategoryId,
            };
        }

    }
}
