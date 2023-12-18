using System.Text;

namespace TakidReciveForm.Domain.Services;

public class AttachmentService : IAttachmentService
{
    public bool IsBase64String(string str)
    {
        var buffer = new Span<byte>(new byte[str.Length]);
        return Convert.TryFromBase64String(str, buffer, out _);
    }

    public string ConvertStringToBase64(string str)
    {
        return Convert.ToBase64String(GetBytes(str));
    }

    public string ConvertBase46ToString(string base64)
    {
        if (IsBase64String(base64.Trim()))
        {
            byte[] bytes = Convert.FromBase64String(base64);
            return GetString(bytes);
        }
        else
        {
            throw new FormatException(nameof(base64));
        }
    }

    public byte[] GetBytes(string str)
    {
        return Encoding.UTF8.GetBytes(str);
    }

    public string GetString(byte[] bytes)
    {
        return Encoding.UTF8.GetString(bytes);
    }

    public byte[] GetBase64Bytes(string base64)
    {
        return Convert.FromBase64String(base64);
    }

    public string GetFilePath(string fileName, string filePath)
    {
        return Path.Combine(filePath, fileName);
    }

    public async Task SaveFileAsync(byte[] bytes, string filePath)
    {
        await File.WriteAllBytesAsync(filePath, bytes);
    }

    public void DeleteFile(string fileName, string filePath)
    {
        File.Delete(GetFilePath(fileName, filePath));
    }
}