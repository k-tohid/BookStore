using BookStore.Core.Domain.Entities;
using BookStore.Core.DTO.CategoryDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Mappings
{
	public static class CategoryMapper
	{
		public static ReadCategoryDTO ToCategoryDTO(this Category category)
		{
			if (category == null)
				throw new ArgumentNullException(nameof(category));

			return new ReadCategoryDTO
			{
				Id = category.Id,
				Name = category.Name,
				Description = category.Description,
			};
		}

		public static IEnumerable<ReadCategoryDTO> ToCategoryDTO(this IEnumerable<Category> categories)
		{
			return categories.Select(c => c.ToCategoryDTO());
		}


		public static Category ToCategory(this ReadCategoryDTO dto)
		{
			if (dto == null)
				throw new ArgumentNullException(nameof(dto));

			return new Category
			{
				Id = dto.Id,
				Name = dto.Name,
				Description = dto.Description,
			};
		}



		public static Category ToCategory(this UpdateCategoryDTO dto)
		{
			return new Category
			{
				Id = dto.Id,
				Name = dto.Name,
				Description = dto.Description
			};
		}

		public static UpdateCategoryDTO ToUpdateCategoryDTO(this ReadCategoryDTO dto)
		{
			return new UpdateCategoryDTO
			{
				Id = dto.Id,
				Name = dto.Name,
				Description = dto.Description
			};
		}
	}
}
