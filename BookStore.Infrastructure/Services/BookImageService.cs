using BookStore.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Services
{
    public class BookImageService : IBookImageService
    {
        
        public bool DeleteBookImage(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            else
            {
                // Log that the file was not found
                return false;
            }
        }

        public async Task<string?> SaveBookImageAsync(IFormFile file, string folderPath)
        {
            if (file == null || file.Length == 0)
                return null;

            // Validate file size and type
            if (file.Length > 5 * 1024 * 1024) // 5 MB limit
                throw new InvalidOperationException("File size exceeds limit.");

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(file.FileName);
            if (!allowedExtensions.Contains(fileExtension.ToLower()))
                throw new InvalidOperationException("Invalid file type.");

            // Save the file
            var uploadFolder = Path.Combine(folderPath);
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadFolder, fileName);

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName; // Returning the file name or relative path
        }
    }
    
}
