using D46S9S_HFT_2023241.Repository;
using D46S9S_HFT_2023241.Models;
using System;
using System.Linq;
using NUnit.Framework;
using D46S9S_HFT_2023241.Logic;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using static D46S9S_HFT_2023241.Logic.OrderLogic;
using System.ComponentModel;
using Moq;

namespace D46S9S_HFT_2023241.Test
{
    
    

    [TestFixture]
    public class Class1
    {
        OrderLogic logic;
        Mock<IRepository<Order>> mockOrderRepo;
        UserLogic user;
        Mock<IRepository<User>> mockUserRepo;
        ProductLogic product;
        Mock<IRepository<Product>> mockProductRepo;
           
       [SetUp]
        public void Test() 
        {
            List<Order> orders = new List<Order>()
            {
                 new Order{OrderId=1,UserId=5,ProductId=4, OrderDate=DateTime.Parse("2023.05.12") },
                new Order{OrderId=2,UserId=4,ProductId=2, OrderDate=DateTime.Parse("2023.11.11") },
                new Order{OrderId=3,UserId=2,ProductId=5, OrderDate=DateTime.Parse("2023.04.05") },
                new Order{OrderId=4,UserId=1,ProductId=1, OrderDate=DateTime.Parse("2023.03.01") },
                new Order{OrderId=5,UserId=3,ProductId=3, OrderDate=DateTime.Parse("2023.12.01") },
                new Order{OrderId=6,UserId=4,ProductId=3, OrderDate=DateTime.Parse("2023.12.01") },


            };

            List<User> users = new List<User>()
            {
                 new User{UserId=1,Username="Bob" },
                new User{UserId=2,Username="John"},
                new User{UserId=3,Username="Adam"},
                new User{UserId=4,Username="Bill"},
                new User{UserId=5,Username="Josh"}
            };

            List<Product> products = new List<Product>()
            {
                new Product{ProductId=1,ProductName="screws",Price= 200},
                new Product{ProductId=2, ProductName="bolts" ,Price = 600},
                new Product{ProductId=3, ProductName="hammer" ,Price = 500},
                new Product{ProductId=4, ProductName="screwdriver" ,Price = 760},
                new Product{ProductId=5, ProductName="nuts" ,Price = 800}
            };
            mockOrderRepo = new Mock<IRepository<Order>>();
            mockOrderRepo.Setup(m => m.ReadAll()).Returns(orders.AsQueryable());
            mockUserRepo = new Mock<IRepository<User>>();
            mockUserRepo.Setup(m => m.ReadAll()).Returns(users.AsQueryable());
            mockProductRepo = new Mock<IRepository<Product>>();
            mockProductRepo.Setup(m => m.ReadAll()).Returns(products.AsQueryable());

            logic = new OrderLogic(mockOrderRepo.Object);

            user = new UserLogic(mockUserRepo.Object);

            product = new ProductLogic(mockProductRepo.Object);


            orders.ForEach(orders => { orders.User = users.Find(x => x.UserId == orders.UserId); });
            orders.ForEach(orders => { orders.Products = products.Find(x => x.ProductId == orders.ProductId); });







        }
        [Test]
        public void DatasTest()
        {
           var name = logic.Datas();
            var exp = new List<Data>
            {
                new Data()
                {
                     Id = 1,
                     Users = 1
                },
                new Data()
                {
                    Id = 2,
                    Users = 1
                },
                new Data()
                {
                    Id = 3,
                    Users = 2
                },
                new Data()
                {
                    Id = 4,
                    Users = 1
                },
                new Data()
                {
                    Id = 5,
                    Users = 1
                }


            };
            
            Assert.AreEqual(name,exp);
            
            
        }
        [Test]
        public void OldestorderTest()
        {

            var act = logic.OldesOrder();

            List<Product> exp = new List<Product> { new Product { ProductId = 1, ProductName="screws",Price =200} };

            
            Assert.AreEqual(act, exp);



        }
        [Test]
        public void BuyerNutsTest()
        {

            var act = logic.BuyersOfNutsID();

            List<int> exp = new List<int> {{ 2 } };


            Assert.AreEqual(act, exp);



        }
        [Test]
        public void MostBuysIDTest()
        {

            var act = logic.MostBuysID();

            List<User> exp = new List<User>{ new User { UserId = 4, Username = "Bill" } };


            Assert.AreEqual(act, exp);



        }
        [Test]
        public void MostSellsIDTest()
        {

            var act = logic.MostSellsID();

            List<Product> exp = new List<Product> { new Product { ProductId = 3, ProductName = "hammer", Price = 500 } };
            
            Assert.AreEqual(act, exp);



        }
        [Test]
        public void OrderCreateTest()
        {
            var order = new Order { OrderId = 9 };
            logic.Create(order);

            mockOrderRepo.Verify(f => f.Create(order), Times.Once);

        }
        [Test]
        public void OrderCreateFalseTest()
        {
            var order = new Order { OrderId = 9 ,OrderDate = DateTime.Parse("2025.11.13") };
            
            try
            {
                logic.Create(order);
            }
            catch 
            {

                
            }

            mockOrderRepo.Verify(f => f.Create(order), Times.Never );

        }
        [Test]
        public void UserCreateTest()
        {
            var userp = new User { UserId = 9 , Username= "Antal"};
            user.Create(userp);

            mockUserRepo.Verify(f => f.Create(userp), Times.Once);

        }
        [Test]
        public void UserCreateFalseTest()
        {
            var usert = new User { UserId = 10,Username ="a"};

            try
            {
                user.Create(usert);
            }
            catch
            {


            }

            mockUserRepo.Verify(f => f.Create(usert), Times.Never);

        }
        [Test]
        public void ProductCreateTest()
        {
            var prod = new Product { ProductName = "csavar", Price = 300 };
            product.Create(prod);

            mockProductRepo.Verify(f => f.Create(prod), Times.Once);

        }
        [Test]
        public void ProductCreateFalseTest()
        {
            var prod = new Product {ProductName ="csavar", Price = -100 };

            try
            {
                product.Create(prod);
            }
            catch
            {


            }

            mockProductRepo.Verify(f => f.Create(prod), Times.Never);

        }





    }
}

