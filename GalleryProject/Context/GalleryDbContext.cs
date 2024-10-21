using GalleryProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GalleryProject.Context
{
    public class GalleryDbContext : DbContext
    {
        public GalleryDbContext(DbContextOptions<GalleryDbContext> options) : base(options)
        {
        }

        public DbSet<GalleryItem> GalleryItems { get; set; }
    }
   
}
