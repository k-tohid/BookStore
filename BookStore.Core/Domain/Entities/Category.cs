using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookStore.Core.Domain.Entities
{
	public class Category
	{
		public int Id { get; set; }

		[Required]
		[StringLength(255)]
		public required string Name { get; set; }

		[StringLength(1000)]
		public string? Description { get; set; }


		// Navigation Property
		public ICollection<Book> Books { get; set; } = new List<Book>();
	}
}
