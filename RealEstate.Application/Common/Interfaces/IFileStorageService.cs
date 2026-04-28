using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RealEstate.Application.Common.Interfaces;

public interface IFileStorageService
{
    Task<string> UploadFileAsync(IFormFile file, string folder);
    Task<bool> DeleteFileAsync(string filePath);
}
