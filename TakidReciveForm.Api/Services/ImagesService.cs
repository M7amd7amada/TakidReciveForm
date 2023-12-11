using TakidReciveForm.Domain.Models;

namespace TakidReciveForm.Api.Services;

public class ImagesService : IImagesService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly string _imagesPath;

    public ImagesService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
        _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
    }

    public void DeleteImage(string imageName)
    {
        var image = Path.Combine(_imagesPath, imageName);
        File.Delete(image);
    }

    public async Task<string> SaveImage(IFormFile image)
    {
        var imageName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";

        var path = Path.Combine(_imagesPath, imageName);

        using var stream = File.Create(path);
        await image.CopyToAsync(stream);

        return imageName;
    }
}