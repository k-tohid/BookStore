using BookStore.Core.Domain.IdentityEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Domain.Entities
{
	public class Cart
	{
		public int Id { get; set; }

		[Required]
		public int UserId { get; set; }

		// Navigation Properties
		public ICollection<CartItem> CartItems { get; set; }

		// Assuming a User model exists
		public ApplicationUser User { get; set; }
	}
}
