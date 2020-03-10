using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using KlioCarProject.Components;
using KlioCarProject.Models;
using Xunit;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;

namespace KlioCarProject.Tests
{
    public class NavigationMenuViewComponentTests
    {
        [Fact]
        public void Can_Select_Categories()
        {
            Mock<ICarRepository> mock = new Mock<ICarRepository>();
            mock.Setup(m => m.Cars).Returns((new Car[]
            {
                new Car { CarID = 1, Model = "P1", Category = "AClass"},
                new Car { CarID = 1, Model = "P1", Category = "AClass"},
                new Car { CarID = 1, Model = "P1", Category = "BClass"},
                new Car { CarID = 1, Model = "P1", Category = "CClass"},
                new Car { CarID = 1, Model = "P1", Category = "AClass"},
            }).AsQueryable<Car>());

            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);

            //Action

            string[] results = ((IEnumerable<string>)(target.Invoke() as ViewViewComponentResult).ViewData.Model).ToArray();

            //Assertion

            Assert.True(Enumerable.SequenceEqual(new string[] { "AClass", "BClass", "CClass" }, results));
        }

        [Fact]
        public void Indicates_Selected_Category()
        {
            string categoryToSelect = "AClass";
            Mock<ICarRepository> mock = new Mock<ICarRepository>();
            mock.Setup(m => m.Cars).Returns((new Car[]
            {
                new Car { CarID = 1, Model = "P1", Category = "AClass"},
                new Car { CarID = 1, Model = "P1", Category = "AClass"},
                new Car { CarID = 1, Model = "P1", Category = "BClass"},
                new Car { CarID = 1, Model = "P1", Category = "CClass"},
                new Car { CarID = 1, Model = "P1", Category = "AClass"},
            }).AsQueryable<Car>());

            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);
            target.ViewComponentContext = new ViewComponentContext()
            {
                ViewContext = new ViewContext
                {
                    RouteData = new RouteData()
                }
            };
            target.RouteData.Values["category"] = categoryToSelect;

            //Action
            string result = (string)(target.Invoke() as ViewViewComponentResult).ViewData["SelectedCategory"];

            //Assertion

            Assert.Equal(categoryToSelect, result);

        }
    }
}
