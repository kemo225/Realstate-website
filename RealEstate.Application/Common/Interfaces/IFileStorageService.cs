using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RealEstate.Application.Common.Interfaces;

public interface IFileStorageService
{
    /// <summary>
    /// Uploads a file and returns the relative path.
    /// </summary>
    Task<string> UploadFileAsync(IFormFile file, string folder);

    /// <summary>
    /// Deletes a file given its relative path or absolute URL.
    /// </summary>
    Task<bool> DeleteFileAsync(string filePath);
}
