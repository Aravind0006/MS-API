using Microsoft.EntityFrameworkCore;
using WebApplication1.Model;

namespace WebApplication1.Dbconnection
{
  public class UserDb : DbContext
  {
    public UserDb(DbContextOptions options) : base(options)
    {
    }
    public DbSet<UploadedFile> UploadedFiles { get; set; }
    public DbSet<ImageUploadModel> ImageUploadModel { get; set; }

  }

}
