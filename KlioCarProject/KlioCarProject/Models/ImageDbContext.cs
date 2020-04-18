using KlioCarProject.Models.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KlioCarProject.Models
{
    public class ImageDbContext : DbContext
    {
        public ImageDbContext(DbContextOptions<ImageDbContext> options) : base(options) 
        {
      
        }
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ImageDbContext context = app.ApplicationServices.GetRequiredService<ImageDbContext>();
            context.Database.EnsureCreated();
        }

        public DbSet<ImageModel> Images { get; set; }
    }
}
