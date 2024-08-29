using BookStore.Core.DTO.CategoryDTO;
using BookStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<ReadCategoryDTO>> GetAllCategoriesAsync();
        Task<ServiceResult> CreateCategoryAsync(CreateCategoryDTO dto);
        Task<ReadCategoryDTO?> GetCategoryByIdAsync(int id);
        Task<ServiceResult> UpdateCategoryAsync(UpdateCategoryDTO dto);
        Task<ServiceResult> DeleteCategoryAsync(int categoryId);


    }
}
