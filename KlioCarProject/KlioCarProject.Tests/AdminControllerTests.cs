using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using KlioCarProject.Models;
using KlioCarProject.Controllers;
using Xunit;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace KlioCarProject.Tests
{
    public class AdminControllerTests
    {
        /*
        [Fact]
        public void Index_Contains_All_Cars()
        {
            Mock<ICarRepository> mock = new Mock<ICarRepository>();
            mock.Setup(m => m.Cars).Returns((new Car[]
            {
                new Car {CarID = 1, Model = "P1"},
                new Car {CarID = 2, Model = "P2"},
                new Car {CarID = 3, Model = "P3"},
            }).AsQueryable<Car>());
            AdminController targer = new AdminController(mock.Object);

            //Action
            Car[] result = GetViewModel<IEnumerable<Car>>(targer.Index())?.ToArray();

            //Assertion

            Assert.Equal(3, result.Length);
            Assert.Equal("P1", result[0].Model);
            Assert.Equal("P2", result[1].Model);
            Assert.Equal("P3", result[2].Model);
        }
        private T GetViewModel<T>(IActionResult result) where T: class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }

        [Fact]
        public void Can_Edit_Car()
        {
            Mock<ICarRepository> mock = new Mock<ICarRepository>();
            mock.Setup(m => m.Cars).Returns((new Car[]
            {
                new Car {CarID = 1, Model = "P1"},
                new Car {CarID = 2, Model = "P2"},
                new Car {CarID = 3, Model = "P3"},
            }).AsQueryable<Car>());
            AdminController targer = new AdminController(mock.Object);

            //Action

            Car p1 = GetViewModel<Car>(targer.Edit(1));
            Car p2 = GetViewModel<Car>(targer.Edit(2));
            Car p3 = GetViewModel<Car>(targer.Edit(3));

            //Assertion

            Assert.Equal(1, p1.CarID);
            Assert.Equal(2, p2.CarID);
            Assert.Equal(3, p3.CarID);
        }
        [Fact]
        public void Can_Delete_Car()
        {
            Car car2 = new Car { CarID = 2, Model = "Tesla" };
            Mock<ICarRepository> mock = new Mock<ICarRepository>();
            mock.Setup(m => m.Cars).Returns((new Car[]
            {
                new Car {CarID = 1, Model = "P1"},
                car2,
                new Car {CarID  = 3, Model = "P3"}
            }).AsQueryable<Car>());

            AdminController target = new AdminController(mock.Object);

            //Action 

            target.Delete(car2.CarID);

            //Assertion
            mock.Verify(m => m.DeleteCar(car2.CarID));
        }
 */
    }
}
