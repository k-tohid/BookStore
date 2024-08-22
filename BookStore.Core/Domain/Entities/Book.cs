using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Domain.Entities
{
	public class Book
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(255)]
		public string Title { get; set; }

		[Required]
		[StringLength(255)]
		public string Author { get; set; }

		[StringLength(1000)]
		public string Description { get; set; }

		[StringLength(255)]
		public string? ImageUrl { get; set; }

		[Required]
		[DataType(DataType.Currency)]
		public decimal Price { get; set; }

		[Required]
		public int CategoryId { get; set; }

		// Navigation Property
		public Category Category { get; set; }
	}
}
