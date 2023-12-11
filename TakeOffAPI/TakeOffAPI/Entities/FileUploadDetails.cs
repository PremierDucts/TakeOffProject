using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("FileUploadDetails")]
public class FileUploadDetails
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; } = 0;
    public string? FileName { get; set; }
    public byte[]? FileData { get; set; }
    public string? FileType { get; set; }
    public bool isUpload { get; set; } = false;
    public int count { get; set; } = 1;
    public FileUploadDetails()
    {
    }

    public FileUploadDetails(FileUploadModelRequest request)
    {
        this.FileName = request.FileName;
        this.FileData = request.FileData;
        this.FileType = request.FileType;
        this.isUpload = false;
    }
}