using Moq;
using System;
using Xunit;
using KlioCarProject.Models;
using KlioCarProject.Controlles;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using KlioCarProject.Models.ViewModels;

namespace KlioCarProject.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Can_Paginate()
        {
            Mock<ICarRepository> mock = new Mock<ICarRepository>();
            mock.Setup(m => m.Cars).Returns((new Car[] {
                new Car { CarID = 1, Model = "P1" },
                new Car { CarID = 2, Model = "P2" },
                new Car { CarID = 3, Model = "P3" },
                new Car { CarID = 4, Model = "P4" },
                new Car { CarID = 5, Model = "P5" }
            }).AsQueryable<Car>());

            //Organization
            CarController controller = new CarController(mock.Object);
            controller.PageSize = 3;

            //Action
            /*IEnumerable<Car> result = controller.List(2).ViewData.Model as IEnumerable<Car>;*/
            CarsListViewModel result = controller.List(2).ViewData.Model as CarsListViewModel;



            Car[] carArray = result.Cars.ToArray();
            Assert.True(carArray.Length == 2);
            Assert.Equal("P4", carArray[0].Model);
            Assert.Equal("P5", carArray[1].Model);

            
        }
    }
}
