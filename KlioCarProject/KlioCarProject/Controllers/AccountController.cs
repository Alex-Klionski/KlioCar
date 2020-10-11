using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KlioCarProject.Models;
using KlioCarProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KlioCarProject.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        private IOrderRepository repository;
        public AccountController(UserManager<AppUser> userMgr, SignInManager<AppUser> signInMgr, IOrderRepository repo)
        {
            userManager = userMgr;
            signInManager = signInMgr;
            IdentitySeedData.EnsurePopulated(userMgr).Wait();
            repository = repo;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            // I added this if
         
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(new LoginModel
                {
                    ReturnUrl = returnUrl
                });
            }
       
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
          
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByNameAsync(loginModel.Name);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    if ((await signInManager.PasswordSignInAsync(user,
                            loginModel.Password, false, false)).Succeeded)
                    {
                        return Redirect(loginModel?.ReturnUrl ?? "/Account/Index");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid name or password");
            return View(loginModel);       
        }
        [Authorize]
        public IActionResult Index() => View(repository.Orders.Where(o=> o.Name == User.Identity.Name && o.Shipped));

        [Authorize(Roles = "Admins")]
        public IActionResult OtherAction() => View("Index", GetData("OtherAction"));
        private Dictionary<string, object> GetData(string actionName) => new Dictionary<string, object>
        {
            ["Action"] = actionName,
            ["User"] = HttpContext.User.Identity.Name,
            ["Authenticated"] = HttpContext.User.Identity.IsAuthenticated,
            ["Auth Type"] = HttpContext.User.Identity.AuthenticationType,
            ["In Users Role"] = HttpContext.User.IsInRole("Admins"),
        };

        [AllowAnonymous]
        public IActionResult AccessDenied() => View();


        [Authorize]
        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }
    }
}