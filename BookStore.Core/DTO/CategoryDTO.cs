using BookStore.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.DTO
{
    public class CategoryDTO
    {
        public int? Id { get; set; }

        [StringLength(255)]
        public string? Name { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        public ICollection<Book>? Books { get; set; }
    }
}
