using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KlioCarProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace KlioCarProject.Controllers
{
    public class OrderController : Controller
    {
        public ViewResult Checkout() => View(new Order());
    }
}