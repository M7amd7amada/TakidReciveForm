namespace TakidReciveForm.Domain.Models;

public class FileSettings
{
    public const string ImagesPath = "/assets/images/form-images";
    public const string AllowedExtensions = ".jpg,.jpeg,.png";
    public const int MaxFileSizeInMB = 3;
    public const int MaxFileSizeInBytes = MaxFileSizeInMB * 1024 * 1024;
}