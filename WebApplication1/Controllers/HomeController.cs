using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApplication1.Dbconnection;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class HomeController : ControllerBase
  {

    private readonly IWebHostEnvironment _environment;
    private readonly UserDb _dbContext;

    public HomeController(IWebHostEnvironment environment, UserDb dbContext)
    {
      _environment = environment;
      _dbContext = dbContext;
    }

    [HttpPost("upload")]
    public IActionResult Upload([FromForm] ImageUploadModel model)
    {
      if (model?.File == null || model.File.Length == 0)
        return BadRequest("Invalid file");

      var uploadsFolder = Path.Combine(_environment.ContentRootPath, "uploads");

      if (!Directory.Exists(uploadsFolder))
      {
        Directory.CreateDirectory(uploadsFolder);
      }

      var fileName = Path.Combine(uploadsFolder, model.File.FileName);

      using (var stream = new FileStream(fileName, FileMode.Create))
      {
        model.File.CopyTo(stream);
      }

      // Save information about the uploaded file to the database
      var uploadedFile = new UploadedFile { FileName = model.File.FileName };
      _dbContext.UploadedFiles.Add(uploadedFile);
      _dbContext.SaveChanges();

      return Ok(new { fileName, uploadedFile.Id });
    }

    [HttpGet("image/{id}")]
    public IActionResult GetImage(int id)
    {
      var uploadedFile = _dbContext.UploadedFiles.Find(id);

      if (uploadedFile == null)
        return NotFound();

      var filePath = Path.Combine(_environment.ContentRootPath, "uploads", uploadedFile.FileName);

      if (!System.IO.File.Exists(filePath))
        return NotFound();

      var fileBytes = System.IO.File.ReadAllBytes(filePath);

      return File(fileBytes, "image/jpeg");
    }

    [HttpGet("all-ids")]
    public IActionResult GetAllImageIds()
    {
      var imageIds = _dbContext.UploadedFiles.Select(uploadedFile => uploadedFile.Id).ToList();
      return Ok(imageIds);
    }
  }
}
