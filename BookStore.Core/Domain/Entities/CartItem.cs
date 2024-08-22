using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Domain.Entities
{
	public class CartItem
	{
		public int Id { get; set; }

		[Required]
		public int CartId { get; set; }

		[Required]
		public int BookId { get; set; }

		[Required]
		public int Quantity { get; set; }

		[Required]
		[DataType(DataType.Currency)]
		public decimal Price { get; set; }

		// Navigation Properties
		public Cart Cart { get; set; }
		public Book Book { get; set; }
	}
}
