using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
		public required string Title { get; set; }

		[Required]
		[StringLength(255)]
		public required string Author { get; set; }

		[StringLength(1000)]
		public string? Summary { get; set; }

		[StringLength(255)]
		public string? ImageUrl { get; set; }

		[Required]
		[DataType(DataType.Currency)]
		[Column(TypeName = "decimal(6,2)")]
		public decimal Price { get; set; }

		public int Stock { get; set; } = 0;



		public int? CategoryId { get; set; }

		// Navigation Property
		public Category? Category { get; set; }
	}
}
