using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KlioCarProject.Models;

namespace KlioCarProject.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private ICarRepository repository;
        public NavigationMenuViewComponent(ICarRepository repository)
        {
            this.repository = repository;
        }
        
        public IViewComponentResult Invoke()
        {
            return View(repository.Cars
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
