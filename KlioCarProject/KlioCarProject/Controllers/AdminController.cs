using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KlioCarProject.Models;

namespace KlioCarProject.Controllers
{
    public class AdminController : Controller
    {
        private ICarRepository repository;
        public AdminController(ICarRepository repo) { repository = repo; }
        public IActionResult Index()
        {
            return View(repository.Cars);
        }
        public ViewResult Edit(int carId) => View(repository.Cars.FirstOrDefault(p => p.CarID == carId));
        [HttpPost]
        public IActionResult Edit(Car car)
        {
            if(ModelState.IsValid)
            {
                repository.SaveCar(car);
                TempData["message"] = $"{car.Model} has been saved";
                return RedirectToAction("Index");
            }
            else
            {
                return View(car);
            }
        }
        public ViewResult Create() => View("Edit", new Car());
        [HttpPost]
        public IActionResult Delete(int carId)
        {
            Car deletedCar = repository.DeleteCar(carId);
            if(deletedCar != null)
            {
                TempData["message"] = $"{deletedCar.Model} was deleted";
            }
            return RedirectToAction("Index");
        }
    }
}