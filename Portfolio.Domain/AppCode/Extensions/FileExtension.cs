using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Portfolio.Domain.AppCode.Extensions
{
    public static partial class Extension
    {
        static public string GetImagePhysicalPath(this IHostEnvironment env, string fileName)
        {
            return Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "images", fileName);
        }

        static public string GetRandomImagePath(this IFormFile file, string prefix = "")
        {

            string extension = Path.GetExtension(file.FileName);

            if (!string.IsNullOrWhiteSpace(prefix))
            {
                prefix = $"{prefix}-";
            }

            return $"{prefix}{Guid.NewGuid()}{extension}".ToLower();
        }

        async static public Task<string> SaveAsync(this IHostEnvironment env, IFormFile file,
            string imageName, CancellationToken cancellationToken)
        {
            
            string fullPath = env.GetImagePhysicalPath(imageName);

            using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                await file.CopyToAsync(fs, cancellationToken);
            }

            return fullPath;
        }


        static public void ArchiveImage(this IHostEnvironment env, string fileName)
        {
            var imageActualPath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "images", fileName);

            if (File.Exists(imageActualPath))
            {
                var imageNewPath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "images", $"archive-{DateTime.Now:yyyyMMddHHmmss}-{fileName}");

                File.Move(imageActualPath, imageNewPath);
            }
        }

        static public bool CheckFileType(this IFormFile file, string type)
        {
            return file.ContentType.Contains(type);
        }
    }
}
