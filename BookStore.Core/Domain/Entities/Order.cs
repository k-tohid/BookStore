using BookStore.Core.Domain.IdentityEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Domain.Entities
{
	public class Order
	{
		public int Id { get; set; }

		[Required]
		public int UserId { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime OrderDate { get; set; }

		[Required]
		[DataType(DataType.Currency)]
		[Column(TypeName = "decimal(6,2)")]
		public decimal TotalAmount { get; set; }

		// Navigation Properties
		public ICollection<OrderItem> OrderItems { get; set; }

		public ApplicationUser User { get; set; }
	}
}
