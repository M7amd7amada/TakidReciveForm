namespace TakidReciveForm.Api.Services;

public interface IImagesService
{
    public Task<string> SaveImage(IFormFile image);
    public void DeleteImage(string imageName);
    public void ConvertToImage(string file, string fileName);
    public string ConvertToBase64(string path);
}