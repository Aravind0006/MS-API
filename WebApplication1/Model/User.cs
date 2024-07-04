using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Model
{
  public class User
  {
  }

  public class UploadedFile
  {
    [Key]
    public int Id { get; set; }
    public string FileName { get; set; }
  }
  public class ImageUploadModel
  {
    [Key]
    public int Id { get; set; }
    [NotMapped]
    public IFormFile File { get; set; }
  }

}
