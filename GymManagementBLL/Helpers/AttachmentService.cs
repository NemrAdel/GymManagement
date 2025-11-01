using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Helpers
{
    public class AttachmentService : IAttachmentService
    {
        private readonly string[] AllowedExtenstions = { ".jpg", ".jpeg", ".png" };
        private readonly long MaxFileSize = 5 * 1024 * 1024;
        private readonly IWebHostEnvironment _webHost;

        public AttachmentService(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }
        public string? Upload(string FolderName, IFormFile file)
        {
            try
            {
                if (FolderName is null || file is null || file.Length == 0)
                    return null;
                if (file.Length > MaxFileSize)
                    return null;
                var extension = Path.GetExtension(file.FileName).ToLower();
                if (!AllowedExtenstions.Contains(extension))
                    return null;
                var FolderPath = Path.Combine(_webHost.WebRootPath, "images", FolderName);
                if (!Directory.Exists(FolderPath))
                {
                    Directory.CreateDirectory(FolderPath);
                }
                var fileName = Guid.NewGuid().ToString() + extension;

                var filePath = Path.Combine(FolderPath, fileName);

                using var fileStream = new FileStream(filePath, FileMode.Create);

                file.CopyTo(fileStream);

                return fileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to Upload Photo in {FolderName} because {ex}");
                return null;
            }
        }
        public bool Delete(string FileName, string FolderName)
        {
            try
            {
                if (string.IsNullOrEmpty(FileName) || string.IsNullOrEmpty(FolderName))
                    return false;
                var fullPath = Path.Combine(_webHost.WebRootPath, "images", FolderName, FileName);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed To Delete File : {ex}");
                return false;
            }
        }

    }
}
