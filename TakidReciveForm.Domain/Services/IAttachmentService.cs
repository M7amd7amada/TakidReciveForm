namespace TakidReciveForm.Domain.Services;

public interface IAttachmentService
{
    public string ConvertStringToBase64(string str);
    public string ConvertBase46ToString(string base64);
    public byte[] GetBytes(string str);
    public string GetString(byte[] bytes);
    public string GetFilePath(string fileName, string filePath);
    public void DeleteFile(string fileName, string filePath);
    public bool IsBase64String(string str);
    public Task SaveFileAsync(byte[] bytes, string filePath);
}