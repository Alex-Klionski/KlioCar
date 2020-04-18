using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KlioCarProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KlioCarProject.Controllers
{
    public class ImageController : Controller
    {
        private ImageDbContext context;
        public ImageController(ImageDbContext _context) { _context = context; }
        public async Task<IActionResult>Index()
        {
            return View(await context.Images.ToListAsync());
        }
    }
}