using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.DTO.CategoryDTO
{
	public class ReadCategoryDTO
	{
		[Required(ErrorMessage = "Id can't be null.")]
		public int Id { get; set; }

		[Required(ErrorMessage = "Name can't be null.")]
		[StringLength(255, MinimumLength = 3, ErrorMessage = "Characters must be greater than 3 and less than 255.")]
		public string Name { get; set; }

		[StringLength(1000, MinimumLength = 3, ErrorMessage = "Characters must be greater than 3 and less than 1000.")]
		public string? Description { get; set; }


		public override string ToString()
		{
			return $"A {base.ToString()}, Name: {Name}, Description: {Description}";
		}
	}
}
