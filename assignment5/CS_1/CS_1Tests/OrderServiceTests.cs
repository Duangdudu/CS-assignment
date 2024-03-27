using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderManagementSystem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderManagementSystem.Tests
{
    [TestClass()]
    public class OrderServiceTests
    {
        [TestMethod()]
        public void AddOrderTest()
        {
            // Arrange
            OrderService orderService = new OrderService();
            Order order = new Order
            {
                ID = 1,
                Customer = "TestCustomer",
                Details = new List<OrderDetail>
                {
                    new OrderDetail { ProductName = "TestProduct", Quantity = 1, OnePrice = 10.0 }
                }
            };

            // Act
            orderService.AddOrder(order);

            // Assert
            Assert.AreEqual(1, orderService.SearchOrderByCustomer("TestCustomer").Count);
        }

        [TestMethod()]
        public void DeleteOrderTest()
        {
            // Arrange
            OrderService orderService = new OrderService();
            Order order = new Order
            {
                ID = 1,
                Customer = "TestCustomer",
                Details = new List<OrderDetail>
                {
                    new OrderDetail { ProductName = "TestProduct", Quantity = 1, OnePrice = 10.0 }
                }
            };
            orderService.AddOrder(order);

            // Act
            orderService.DeleteOrder(1);

            // Assert
            Assert.AreEqual(0, orderService.SearchOrderByCustomer("TestCustomer").Count);
        }

        [TestMethod()]
        public void ModifyOrderTest()
        {
            // Arrange
            OrderService orderService = new OrderService();
            Order order = new Order
            {
                ID = 1,
                Customer = "TestCustomer",
                Details = new List<OrderDetail>
                {
                    new OrderDetail { ProductName = "TestProduct", Quantity = 1, OnePrice = 10.0 }
                }
            };
            orderService.AddOrder(order);

            Order modifiedOrder = new Order
            {
                ID = 1,
                Customer = "ModifiedCustomer",
                Details = new List<OrderDetail>
                {
                    new OrderDetail { ProductName = "ModifiedProduct", Quantity = 2, OnePrice = 20.0 }
                }
            };

            // Act
            orderService.ModifyOrder(modifiedOrder);

            // Assert
            Order queriedOrder = orderService.SearchOrderByID(1);
            Assert.AreEqual("ModifiedCustomer", queriedOrder.Customer);
            Assert.AreEqual("ModifiedProduct", queriedOrder.Details[0].ProductName);
            Assert.AreEqual(2, queriedOrder.Details[0].Quantity);
            Assert.AreEqual(20.0, queriedOrder.Details[0].OnePrice);
        }

        [TestMethod()]
        public void SearchOrderByIDTest()
        {
            // Arrange
            OrderService orderService = new OrderService();
            Order order = new Order
            {
                ID = 1,
                Customer = "TestCustomer",
                Details = new List<OrderDetail>
                {
                    new OrderDetail { ProductName = "TestProduct", Quantity = 1, OnePrice = 10.0 }
                }
            };
            orderService.AddOrder(order);

            // Act
            Order searchedOrder = orderService.SearchOrderByID(1);

            // Assert
            Assert.IsNotNull(searchedOrder);
            Assert.AreEqual(1, searchedOrder.ID);
        }

        [TestMethod()]
        public void SearchOrderByProductNameTest()
        {
            // Arrange
            OrderService orderService = new OrderService();
            Order order1 = new Order
            {
                ID = 1,
                Customer = "TestCustomer",
                Details = new List<OrderDetail>
        {
            new OrderDetail { ProductName = "TestProduct1", Quantity = 1, OnePrice = 10.0 }
        }
            };
            Order order2 = new Order
            {
                ID = 2,
                Customer = "TestCustomer",
                Details = new List<OrderDetail>
        {
            new OrderDetail { ProductName = "TestProduct2", Quantity = 2, OnePrice = 20.0 }
        }
            };
            orderService.AddOrder(order1);
            orderService.AddOrder(order2);

            // Act
            List<Order> searchedOrders = orderService.SearchOrderByProductName("TestProduct1");

            // Assert
            Assert.AreEqual(1, searchedOrders.Count);
            Assert.AreEqual("TestProduct1", searchedOrders[0].Details[0].ProductName);
        }

        [TestMethod()]
        public void SearchOrderByPriceTest()
        {
            // Arrange
            OrderService orderService = new OrderService();
            Order order1 = new Order
            {
                ID = 1,
                Customer = "TestCustomer",
                Details = new List<OrderDetail>
        {
            new OrderDetail { ProductName = "TestProduct1", Quantity = 1, OnePrice = 10.0 }
        }
            };
            Order order2 = new Order
            {
                ID = 2,
                Customer = "TestCustomer",
                Details = new List<OrderDetail>
        {
            new OrderDetail { ProductName = "TestProduct2", Quantity = 2, OnePrice = 20.0 }
        }
            };
            orderService.AddOrder(order1);
            orderService.AddOrder(order2);

            // Act
            List<Order> searchedOrders = orderService.SearchOrderByPrice(40.0);

            // Assert
            Assert.AreEqual(1, searchedOrders.Count);
            Assert.AreEqual(2, searchedOrders[0].ID);
        }

        [TestMethod()]
        public void SortOrdersTest()
        {
            // Arrange
            OrderService orderService = new OrderService();
            Order order1 = new Order
            {
                ID = 1,
                Customer = "TestCustomer1",
                Details = new List<OrderDetail>
        {
            new OrderDetail { ProductName = "TestProduct1", Quantity = 1, OnePrice = 10.0 }
        }
            };
            Order order2 = new Order
            {
                ID = 2,
                Customer = "TestCustomer2",
                Details = new List<OrderDetail>
        {
            new OrderDetail { ProductName = "TestProduct2", Quantity = 2, OnePrice = 20.0 }
        }
            };
            orderService.AddOrder(order1);
            orderService.AddOrder(order2);

            // Act
            orderService.SortOrders(o => o.Customer);

            // Assert
            Assert.AreEqual("TestCustomer1", orderService.SearchOrderByID(1).Customer);
            Assert.AreEqual("TestCustomer2", orderService.SearchOrderByID(2).Customer);
        }

    }
}
