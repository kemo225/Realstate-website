using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Exceptions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RealEstate.Infrastructure.Storage;

public class ImageService : IImageService
{
    private readonly IWebHostEnvironment _environment;
    private const int MaxWidth = 1200;
    private const int Quality = 75;

    public ImageService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string> UploadAsync(IFormFile file, string folder)
    {
        if (file == null || file.Length == 0)
            return string.Empty;

        if (!file.ContentType.StartsWith("image/"))
            throw new ValidatationException("Invalid image file.");

        var root = _environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        var imagesFolder = Path.Combine(root, "images", folder);

        if (!Directory.Exists(imagesFolder))
            Directory.CreateDirectory(imagesFolder);

        var fileName = $"{Guid.NewGuid()}.webp";
        var filePath = Path.Combine(imagesFolder, fileName);

        using (var image = await Image.LoadAsync(file.OpenReadStream()))
        {
            if (image.Width > MaxWidth)
            {
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(MaxWidth, 0),
                    Mode = ResizeMode.Max
                }));
            }

            var encoder = new WebpEncoder
            {
                Quality = Quality
            };

            await image.SaveAsync(filePath, encoder);
        }

        return $"images/{folder}/{fileName}";
    }

    public Task DeleteAsync(string imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath))
            return Task.CompletedTask;

        string relativePath = imagePath;
        if (imagePath.StartsWith("http", StringComparison.OrdinalIgnoreCase))
        {
            var uri = new Uri(imagePath);
            relativePath = uri.AbsolutePath.TrimStart('/');
        }
        else
        {
            relativePath = imagePath.TrimStart('/');
        }

        var root = _environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        var fullPath = Path.Combine(root, relativePath);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

        return Task.CompletedTask;
    }
}
