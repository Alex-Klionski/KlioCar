using Moq;
using System;
using Xunit;
using KlioCarProject.Models;
using KlioCarProject.Controlles;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using KlioCarProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KlioCarProject.Tests
{
    public class CarControllerTests
    {
        [Fact]
        public void Can_Paginate()
        {
            Mock<ICarRepository> mock = new Mock<ICarRepository>();
            mock.Setup(m => m.Cars).Returns((new Car[] {
                new Car { CarID = 1, Model = "P1", Category = "AClass" },
                new Car { CarID = 2, Model = "P2", Category = "CClass" },
                new Car { CarID = 3, Model = "P3", Category = "CClass" },
                new Car { CarID = 4, Model = "P4", Category = "CClass" },
                new Car { CarID = 5, Model = "P5", Category = "CClass" }
            }).AsQueryable<Car>());

            //Organization
            CarController controller = new CarController(mock.Object);
            controller.PageSize = 3;

            //Action
            // IEnumerable<Car> result = controller.List(2).ViewData.Model as IEnumerable<Car>;
            CarsListViewModel result = controller.List(null, 2).ViewData.Model as CarsListViewModel;



            Car[] carArray = result.Cars.ToArray();
            Assert.True(carArray.Length == 2);
            Assert.Equal("P4", carArray[0].Model);
            Assert.Equal("P5", carArray[1].Model);


        }

        [Fact]
        public void Can_Filter_Category()
        {
            Mock<ICarRepository> mock = new Mock<ICarRepository>();
            mock.Setup(m => m.Cars).Returns((new Car[] {
                new Car { CarID = 1, Model = "P1", Category = "AClass" },
                new Car { CarID = 2, Model = "P2", Category = "CClass" },
                new Car { CarID = 3, Model = "P3", Category = "CClass" },
                new Car { CarID = 4, Model = "P4", Category = "AClass" },
                new Car { CarID = 5, Model = "P5", Category = "CClass" }
            }).AsQueryable<Car>());

            //Organization
            CarController controller = new CarController(mock.Object);
            controller.PageSize = 3;

            //Action

            Car[] result = (controller.List("AClass", 1).ViewData.Model as CarsListViewModel).Cars.ToArray();


            //Assertion
            Assert.Equal(2, result.Length);
            Assert.True(result[0].Model == "P1", result[0].Category = "AClass");
            Assert.True(result[1].Model == "P4", result[0].Category = "AClass");


        }

        [Fact]
        public void GenerateCategoryCount()
        {
            Mock<ICarRepository> mock = new Mock<ICarRepository>();
            mock.Setup(m => m.Cars).Returns((new Car[] {
                new Car { CarID = 1, Model = "P1", Category = "AClass" },
                new Car { CarID = 2, Model = "P2", Category = "CClass" },
                new Car { CarID = 3, Model = "P3", Category = "CClass" },
                new Car { CarID = 4, Model = "P4", Category = "AClass" },
                new Car { CarID = 5, Model = "P5", Category = "BClass" },
                new Car { CarID = 5, Model = "P5", Category = "CClass" }
                }).AsQueryable<Car>());

            CarController target = new CarController(mock.Object);
            target.PageSize = 3;
            Func<ViewResult, CarsListViewModel> GetModel = result => result?.ViewData?.Model as CarsListViewModel;

            //Action

            int? res1 = GetModel(target.List("AClass"))?.PagingInfo.TotalItems;
            int? res2 = GetModel(target.List("BClass"))?.PagingInfo.TotalItems;
            int? res3 = GetModel(target.List("CClass"))?.PagingInfo.TotalItems;
            int? resAll = GetModel(target.List(null))?.PagingInfo.TotalItems;

            //Assertion
            Assert.Equal(2, res1);
            Assert.Equal(1, res2);
            Assert.Equal(3, res3);
            //Assert.Equal(5, resAll);

        }
    }
}
