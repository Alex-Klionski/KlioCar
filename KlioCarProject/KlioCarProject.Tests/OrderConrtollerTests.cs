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

        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            Cart cart = new Cart();
            Order order = new Order();
            OrderController target = new OrderController(mock.Object, cart);


            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
        }

    }
}
