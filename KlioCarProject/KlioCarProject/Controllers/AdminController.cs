using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KlioCarProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using KlioCarProject.Models.ViewModels;

namespace KlioCarProject.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private ICarRepository repository;
        private UserManager<AppUser> userManager;
        public AdminController(ICarRepository repo, UserManager<AppUser> usrMng) 
        {
            repository = repo; 
            userManager = usrMng; 
        }
        public ViewResult Users() => View(userManager.Users);
        public IActionResult Index()
        {
            return View(repository.Cars);
        }
        public ViewResult CreateUser() => View();

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser(model.Name)
                {
                    UserName = model.Name,
                    Email = model.Email
                };
                IdentityResult result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return View("Index", userManager.Users);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

        public ViewResult EditCar(int carId) => View(repository.Cars.FirstOrDefault(p => p.CarID == carId));
        [HttpPost]
        public IActionResult EditCar(Car car)
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
        public ViewResult CreateCar() => View("EditCar", new Car());
        [HttpPost]
        public IActionResult DeleteCar(int carId)
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