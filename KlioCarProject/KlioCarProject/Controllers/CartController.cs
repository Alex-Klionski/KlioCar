using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KlioCarProject.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using KlioCarProject.Infrastructure;
using KlioCarProject.Models.ViewModels;

namespace KlioCarProject.Controllers
{
    public class CartController : Controller
    {
        private ICarRepository repository;
        public CartController(ICarRepository repository) { this.repository = repository; }
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewМodel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }
        public RedirectToActionResult AddToCart(int carID, string returnUrl)
        {
            Car car = repository.Cars.FirstOrDefault(p => p.CarID == carID);

            if (car != null)
            {
                Cart cart = GetCart();
                cart.AddItem(car, 1);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int carID, string returnUrl)
        {
            Car car = repository.Cars.FirstOrDefault(p => p.CarID == carID);

            if (car != null)
            {
                Cart cart = GetCart();
                cart.RemoveLine(car);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();

            return cart;
        }
        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
    }

}