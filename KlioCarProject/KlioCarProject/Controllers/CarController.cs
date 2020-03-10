using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KlioCarProject.Models;
using KlioCarProject.Models.ViewModels;

namespace KlioCarProject.Controlles
{
    public class CarController : Controller
    {
        private ICarRepository repository;
        public int PageSize = 4;
        public CarController(ICarRepository repository) { this.repository = repository; }
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

      /*  public ViewResult List(int productPage = 1) 
            => View(repository.Cars
            .OrderBy(p => p.CarID)
            .Skip((productPage - 1) * PageSize)
            .Take(PageSize));*/
    }
}