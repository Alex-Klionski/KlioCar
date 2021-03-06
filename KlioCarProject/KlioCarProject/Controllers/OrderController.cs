﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KlioCarProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MimeKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace KlioCarProject.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private IOrderRepository repository;
        private UserManager<AppUser> userManager;
        private Cart cart;
        private readonly IConfiguration _configuration;

        [ActivatorUtilitiesConstructor]
        public OrderController(IOrderRepository repoService, Cart cartService, UserManager<AppUser> usrMng, IConfiguration configuration)
        {
            repository = repoService;
            cart = cartService;
            this._configuration = configuration;
            userManager = usrMng;
        }
        public OrderController(IOrderRepository repoService, Cart cartService)
        {
            repository = repoService;
            cart = cartService;
        }
        [Authorize(Roles = "Admins, Managers")]
        public ViewResult List()
        {
            return View(repository.Orders.Where(o => !o.Shipped));
        }

        [HttpPost]
        [Authorize(Roles ="Admins, Managers")]

        public async Task<IActionResult> MarkShipped(int orderID)
        {
            Order order = repository.Orders.FirstOrDefault(o => o.OrderID == orderID);
            if (order != null)
            {
                order.Shipped = true;
                if (User.Identity.IsAuthenticated)
                {
                    MimeMessage message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Order KlioCar", "Congrats"));
                    AppUser user = await userManager.FindByNameAsync(order.Name);
                    message.To.Add(new MailboxAddress(user.Email));
                    string model = null;
                    foreach (CartLine line in order.Lines)
                    {
                        model = line.Car.Model;
                    }
                    message.Subject = "Your order is approved " +  order.Name + " # " + order.OrderID;
                    message.Body = new BodyBuilder() { HtmlBody = "Your order is approved " + order.Total + " " + model + "<div style=\"color:green;\">Message from Kliocar </div>" }.ToMessageBody();

                    using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 465, true);
                        client.Authenticate(_configuration["EmailData:Email"], _configuration["EmailData:Password"]);
                        client.Send(message);
                        client.Disconnect(true);
                    }

                }
                repository.SaveOrder(order);
            }
            return RedirectToAction(nameof(List));
        }
        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty");
            }
            if (ModelState.IsValid)
            {
                order.Total = cart.ComputeTotalValue(); // total
                order.Lines = cart.Lines.ToArray();
                repository.SaveOrder(order);
                if (User.Identity.IsAuthenticated)
                {
                    MimeMessage message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Order KlioCar", "Congrats"));
                    AppUser user = await userManager.FindByNameAsync(User.Identity.Name);
                    message.To.Add(new MailboxAddress(user.Email));

                    string model = null;
                    foreach (CartLine line in order.Lines)
                    {
                        model = line.Car.Model;
                    }
                    message.Subject = "Order" + " " + order.Name + " # " + order.OrderID;
                    message.Body = new BodyBuilder() { HtmlBody = order.Total + " " + model + "<div style=\"color:green;\">Message from Kliocar </div>" }.ToMessageBody();

                    using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 465, true);
                        client.Authenticate(_configuration["EmailData:Email"], _configuration["EmailData:Password"]);
                        client.Send(message);
                        client.Disconnect(true);
                    }

                }
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }
        public ViewResult Completed()
        {
            cart.Clear();
            return View();
        }
    }
}