using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Domain.Entities
{
	public class Category
	{
		public int Id { get; set; }

		[Required]
		[StringLength(255)]
		public string Name { get; set; }

		[StringLength(1000)]
		public string Description { get; set; }

		// Navigation Property
		public ICollection<Book> Books { get; set; }
	}
}
