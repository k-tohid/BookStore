using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Domain.Entities
{
	public class OrderItem
	{
		public int Id { get; set; }

		[Required]
		public int OrderId { get; set; }

		[Required]
		public int BookId { get; set; }

		[Required]
		public int Quantity { get; set; }

		[Required]
		[DataType(DataType.Currency)]
		public decimal Price { get; set; }

		// Navigation Properties
		public Order Order { get; set; }
		public Book Book { get; set; }
	}
}
