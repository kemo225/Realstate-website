using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RealEstate.Application.Common.Interfaces;

namespace RealEstate.Infrastructure.Storage;

public class LocalFileStorageService : IFileStorageService
{
    private readonly IWebHostEnvironment _environment;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public LocalFileStorageService(IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor)
    {
        _environment = environment;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> UploadFileAsync(IFormFile file, string folder)
    {
        if (file == null || file.Length == 0) return string.Empty;

        var root = _environment.WebRootPath;

        if (string.IsNullOrEmpty(root))
        {
            root = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        }

        var uploadsFolder = Path.Combine(root, "uploads", folder);

        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var request = _httpContextAccessor!.HttpContext!.Request;
        var baseUrl = $"{request.Scheme}://{request.Host}";

        return $"{baseUrl}/uploads/{folder}/{fileName}";
    }

    public async Task<bool> DeleteFileAsync(string fileUrl)
    {
        if (string.IsNullOrWhiteSpace(fileUrl))
            return false;

        var uri = new Uri(fileUrl);
        var relativePath = uri.AbsolutePath.TrimStart('/');

        var root = _environment.WebRootPath;

        if (string.IsNullOrEmpty(root))
        {
            root = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        }

        var fullPath = Path.Combine(root, relativePath);

        if (!File.Exists(fullPath))
            return false;

        File.Delete(fullPath);
        return true;
    }
}
