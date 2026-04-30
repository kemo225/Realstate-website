using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace RealEstate.Application.Common.Interfaces;

public interface IImageService
{
    /// <summary>
    /// Validates, resizes, converts to WebP, and saves the image.
    /// </summary>
    /// <param name="file">The image file to upload.</param>
    /// <param name="folder">The target folder inside wwwroot/images/.</param>
    /// <returns>The relative path to the saved image.</returns>
    Task<string> UploadAsync(IFormFile file, string folder);

    /// <summary>
    /// Deletes the image from the file system.
    /// </summary>
    /// <param name="imagePath">The relative path or full URL of the image.</param>
    Task DeleteAsync(string imagePath);
}
