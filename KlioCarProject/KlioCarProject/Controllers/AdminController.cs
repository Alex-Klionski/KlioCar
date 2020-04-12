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
    [Authorize(Roles ="Admins")]
    public class AdminController : Controller
    {
        private ICarRepository repository;
        private UserManager<AppUser> userManager;

        private IUserValidator<AppUser> userValidator;
        private IPasswordValidator<AppUser> passwordValidator;
        private IPasswordHasher<AppUser> passwordHasher;


        public AdminController(ICarRepository repo, UserManager<AppUser> usrMng, IPasswordValidator<AppUser> passValid, IPasswordHasher<AppUser> passHasher, IUserValidator<AppUser> userValid)
        {
            repository = repo; 
            userManager = usrMng;
            passwordValidator = passValid;
            userValidator = userValid;
            passwordHasher = passHasher;
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
        public async Task<IActionResult>DeleteUser(string id)
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
        public async Task<IActionResult>EditUser(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if(user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]                     
        public async Task<IActionResult>EditUser(string id, string email, string password)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if(user != null)
            {
                user.Email = email;
                IdentityResult validEmail = await userValidator.ValidateAsync(userManager, user);
                if(!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }
                IdentityResult validPass = null;
                if (!string.IsNullOrEmpty(password))
                {
                    validPass = await passwordValidator.ValidateAsync(userManager, user, password);
                    if (validPass.Succeeded)
                        user.PasswordHash = passwordHasher.HashPassword(user, password);
                    else
                        AddErrorsFromResult(validPass);
                }
                if ((validEmail.Succeeded && validPass == null) || (validEmail.Succeeded && password != string.Empty && validPass.Succeeded))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    else
                        AddErrorsFromResult(result);
                }   
            }
            else
            {
                ModelState.AddModelError("", "User not Founded");
            }
            return View(user);
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

        //ERRORS
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

    }
}