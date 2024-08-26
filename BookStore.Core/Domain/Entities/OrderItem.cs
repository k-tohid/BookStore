﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
		[Column(TypeName = "decimal(6,2)")]
		public decimal Price { get; set; }

		// Navigation Properties
		public Order Order { get; set; }
		public Book Book { get; set; }
	}
}
