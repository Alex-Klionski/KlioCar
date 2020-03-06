using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using KlioCarProject.Models;
using KlioCarProject.Components;
using Moq;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ViewComponents;

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
    }
}
