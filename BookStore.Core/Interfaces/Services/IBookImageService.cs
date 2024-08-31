using BookStore.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Interfaces.Services
{
    public interface IBookImageService
    {
        Task<string?> SaveBookImageAsync(IFormFile file, string folderPath);
        bool DeleteBookImage(string filePath);
    }
}
