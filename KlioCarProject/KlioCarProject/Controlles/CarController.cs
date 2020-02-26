using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KlioCarProject.Models;

namespace KlioCarProject.Controlles
{
    public class CarController : Controller
    {
        private ICarRepository repository;
        public CarController(ICarRepository repository) { this.repository = repository; }
        public ViewResult List() => View(repository.Cars);
    }
}