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
    public class CartTests
    {
        [Fact]
        public void Can_Add_Quantity_For_Existing_Line()
        {
            Car p1 = new Car { CarID = 1, Model = "P1" };
            Car p2 = new Car { CarID = 2, Model = "P2" };

            Cart target = new Cart();

            //Action
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] results = target.Lines.ToArray();

            //Assertion
            Assert.Equal(p1, results[0].Car);
            Assert.Equal(p2, results[1].Car);
            Assert.Equal(2, results.Length);


        }

        [Fact]
        public void Can_Remove_Line()
        {
            Car p1 = new Car { CarID = 1, Model = "P1" };
            Car p2 = new Car { CarID = 2, Model = "P2" };
            Car p3 = new Car { CarID = 3, Model = "P3" };

            Cart target = new Cart();

            //Action
            target.AddItem(p1, 1);
            target.AddItem(p2, 7);
            target.AddItem(p3, 8);
            target.AddItem(p2, 1);

            target.RemoveLine(p2);



            //Assertion
            Assert.Empty(target.Lines.Where(p => p.Car == p2));
            Assert.Equal(2, target.Lines.Count());


        }
    }
}
