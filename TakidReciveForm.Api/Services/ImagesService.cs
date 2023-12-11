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

    public string ConvertToBase64(string path)
    {
        if (File.Exists(path))
        {
            byte[] imageBytes = File.ReadAllBytes(path);
            string base64 = Convert.ToBase64String(imageBytes);
            return base64;
        }
        else
        {
            throw new FileNotFoundException($"Image file not found at path: {path}");
        }
    }

    public void ConvertToImage(string base64, string fileName)
    {
        byte[] bytes = Convert.FromBase64String(base64);
        string filePath = Path.Combine(_imagesPath, fileName);
        File.WriteAllBytes(filePath, bytes);
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