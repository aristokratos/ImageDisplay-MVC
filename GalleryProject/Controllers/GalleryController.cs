using GalleryProject.Context;
using GalleryProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace GalleryProject.Controllers
{
    public class GalleryController : Controller
    {
        private readonly GalleryDbContext _context;

        public GalleryController(GalleryDbContext context)
        {
            _context = context;
        }

        
        public IActionResult Index()
        {
            var galleryItems = _context.GalleryItems.ToList();
            return View(galleryItems);
        }

       
        public IActionResult Gallery()
        {
            var galleryItems = _context.GalleryItems.ToList();
            return View(galleryItems);
        }
        [HttpGet]
        public IActionResult Upload()
        {
            return View(); 
        }



        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var sanitizedFileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine("wwwroot/images", sanitizedFileName);

                
                if (!Directory.Exists("wwwroot/images"))
                {
                    Directory.CreateDirectory("wwwroot/images");
                }

                
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                
                var galleryItem = new GalleryItem
                {
                    FileName = sanitizedFileName,
                    FilePath = "/images/" + sanitizedFileName,
                    UploadDate = DateTime.Now
                };

                _context.GalleryItems.Add(galleryItem);
                await _context.SaveChangesAsync();

                
                return RedirectToAction("Index");
            }

            
            ModelState.AddModelError("", "Please select a file to upload.");
            return View();
        }
    }
}
