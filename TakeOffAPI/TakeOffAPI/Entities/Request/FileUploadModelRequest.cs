using System.Runtime.InteropServices;

public class FileUploadModelRequest
{
    //public IFormFile FileDetail {  get; set; }

    public string? FileName { get; set; }
    public byte[]? FileData { get; set; }
    public string? FileType { get; set; }
}