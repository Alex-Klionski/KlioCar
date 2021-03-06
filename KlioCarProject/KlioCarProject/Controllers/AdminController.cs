﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KlioCarProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using KlioCarProject.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace KlioCarProject.Controllers
{
    [Authorize(Roles ="Admins, Managers")]
    public class AdminController : Controller
    {
        private ICarRepository repository;
        private UserManager<AppUser> userManager;

        private IUserValidator<AppUser> userValidator;
        private IPasswordValidator<AppUser> passwordValidator;
        private IPasswordHasher<AppUser> passwordHasher;
        private readonly IConfiguration _configuration;


        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ApplicationDbContext context;

        [ActivatorUtilitiesConstructor]
        public AdminController(ICarRepository repo, UserManager<AppUser> usrMng, IPasswordValidator<AppUser> passValid, IPasswordHasher<AppUser> passHasher, IUserValidator<AppUser> userValid, IWebHostEnvironment hostEnvironment, ApplicationDbContext _context, IConfiguration configuration)
        {
            repository = repo; 
            userManager = usrMng;
            passwordValidator = passValid;
            userValidator = userValid;
            passwordHasher = passHasher;
            this._configuration = configuration;
            this._hostEnvironment = hostEnvironment;
            context = _context;
        }
        public AdminController(ICarRepository repo)
        {
            repository = repo;
        }

        public ViewResult Users() => View(userManager.Users);

        public IActionResult Index()
        {
            return View(repository.Cars);
        }
        [AllowAnonymous]
        public ViewResult CreateUser() => View();

        [AllowAnonymous]
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
        [Authorize(Roles = "Admins")]
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

        [Authorize(Roles = "Admins")]
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

        [Authorize(Roles = "Admins")]
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

        [Authorize(Roles = "Admins")]
        public ViewResult EditCar(int carId) => View(repository.Cars.FirstOrDefault(p => p.CarID == carId));


        [Authorize(Roles = "Admins")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCar(Car car)
        {
            if(ModelState.IsValid)
            {

                if (car.ImageFile != null && car.ImageFile.Length > 0)
                {
                    var imagePath = @_configuration["ImageSettings:Path"];
                    var uploadPath = _hostEnvironment.WebRootPath + imagePath;

                    //Create Directory
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    //Create Uniq file name
                    var uniqFileName = Guid.NewGuid().ToString();
                    var fileName = Path.GetFileName(uniqFileName + "." + car.ImageFile.FileName.Split(".")[1].ToLower());
                    car.ImageName = fileName;
                    string fullPath = uploadPath + fileName;
                    imagePath = imagePath + @"\";
                    var filePath = @".." + Path.Combine(imagePath, fileName);

                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        car.ImageFile.CopyTo(fileStream);
                    }
                    //Insert record
                    //context.Images.Add(imageModel);
                    //await context.SaveChangesAsync();

                    repository.SaveCar(car);
                    TempData["message"] = $"{car.Model} has been saved";

                    return RedirectToAction(nameof(Index));
                }


                return RedirectToAction("Index");
            }
            else
            {
                return View(car);
            }
        }

        [Authorize(Roles = "Admins")]
        public ViewResult CreateCar() => View("EditCar", new Car());

        [Authorize(Roles = "Admins")]
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