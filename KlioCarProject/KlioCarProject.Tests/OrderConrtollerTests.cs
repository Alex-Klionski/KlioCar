using System;
using System.Collections.Generic;
using System.Text;
using KlioCarProject.Models;
using KlioCarProject.Controllers;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace KlioCarProject.Tests
{
    public class OrderConrtollerTests
    {
        /*
        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            Cart cart = new Cart();
            Order order = new Order();
            OrderController target = new OrderController(mock.Object, cart);

            //Action

            ViewResult result = target.Checkout(order) as ViewResult;

            //Assertion

            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            Assert.False(result.ViewData.ModelState.IsValid);

        }
        [Fact]
        public void Can_Checkout_And_Submit_Order()
        {
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            Cart cart = new Cart();
            cart.AddItem(new Car(), 1);
            OrderController target = new OrderController(mock.Object, cart);

            //Action

            RedirectToActionResult result = target.Checkout(new Order()) as RedirectToActionResult;

            //Assertion

            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once);
            Assert.Equal("Completed", result.ActionName);
        }
        */
    }
}
