using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Application.Features.Projects.Commands.UploadProjectImages
{
    public class UploadProjectImagesCommandValidator
      : AbstractValidator<UploadProjectImagesCommand>
    {
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };
        private const int MaxFileSizeInMB = 5;

        public UploadProjectImagesCommandValidator()
        {
            // ProjectId
            RuleFor(x => x.ProjectId)
                .GreaterThan(0).WithMessage("ProjectId must be valid");

            // Files required
            RuleFor(x => x.Files)
                .NotNull().WithMessage("Files are required")
                .NotEmpty().WithMessage("At least one image is required");

            // Max number of files
            RuleFor(x => x.Files)
                .Must(files => files.Count <= 10)
                .WithMessage("Maximum 10 images allowed");

            // Validate each file
            RuleForEach(x => x.Files)
                .NotNull()
                .Must(BeValidImage)
                .WithMessage("Invalid image file. Only jpg, jpeg, png, webp are allowed and max size is 5MB");
        }

        private bool BeValidImage(IFormFile file)
        {
            if (file == null) return false;

            // Check size
            if (file.Length > MaxFileSizeInMB * 1024 * 1024)
                return false;

            // Check extension
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!_allowedExtensions.Contains(extension))
                return false;

            return true;
        }
    }
}
