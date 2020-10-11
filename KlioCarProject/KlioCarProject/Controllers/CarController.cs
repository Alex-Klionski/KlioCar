using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KlioCarProject.Models;
using KlioCarProject.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using KlioCarProject.Infrastructure;

namespace KlioCarProject.Controlles
{
    public class CarController : Controller
    {
        public int PageSize = 4;
        private ICarRepository repository;
        public readonly UserManager<AppUser> _userManager;
        public readonly AppIdentityDbContext _context;

        public CarController(ICarRepository repository, UserManager<AppUser> userManager, AppIdentityDbContext context) 
        { 
            this.repository = repository;
            _userManager = userManager;
            _context = context;
        }
        public ViewResult List(string category, int productPage = 1)
            =>View(new CarsListViewModel
            {
                Cars = repository.Cars
                .Where(p => category == null || p.Category == category) // fix 
                .OrderBy(p => p.CarID)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? repository.Cars.Count() : repository.Cars.Where(e => e.Category == category).Count()
                },
                CurrentCategory = category
            });
        public ViewResult ChatList(int carID) => View(repository.Cars.FirstOrDefault(p => p.CarID == carID));

        public ViewResult Details(int carID) => View(repository.Cars.FirstOrDefault(p => p.CarID == carID));
        /*  public ViewResult List(int productPage = 1) 
              => View(repository.Cars
              .OrderBy(p => p.CarID)
              .Skip((productPage - 1) * PageSize)
              .Take(PageSize));*/

    }
}