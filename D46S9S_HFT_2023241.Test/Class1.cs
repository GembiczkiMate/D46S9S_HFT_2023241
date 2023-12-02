using D46S9S_HFT_2023241.Repository;
using D46S9S_HFT_2023241.Models;
using System;
using System.Linq;
using NUnit.Framework;
using D46S9S_HFT_2023241.Logic;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using static D46S9S_HFT_2023241.Logic.OrderLogic;

namespace D46S9S_HFT_2023241.Test
{
    public class Fake : IRepository<Order>

    {
        public void Create(Order item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Order Read(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Order> ReadAll()
        {
            return new List<Order>()
            {
                new Order{OrderId=1,UserId=5,ProductId=4, OrderDate=DateTime.Parse("2023.05.12") },
                new Order{OrderId=2,UserId=4,ProductId=2, OrderDate=DateTime.Parse("2023.11.11") },
                new Order{OrderId=3,UserId=2,ProductId=5, OrderDate=DateTime.Parse("2023.04.05") },
                new Order{OrderId=4,UserId=1,ProductId=1, OrderDate=DateTime.Parse("2023.03.01") },
                new Order{OrderId=5,UserId=3,ProductId=3, OrderDate=DateTime.Parse("2023.12.01") },
                new Order{OrderId=6,UserId=4,ProductId=3, OrderDate=DateTime.Parse("2023.12.01") }


            }.AsQueryable();
        }

        public void Update(Order item)
        {
            throw new NotImplementedException();
        }
    }
    [TestFixture]
    public class Class1
    {
        OrderLogic logic;
        [SetUp]
        public void Test() 
        {      

           logic = new OrderLogic(new Fake());

        }
        [Test]
        public void DatasTest()
        {
           var name = logic.Datas().ToList();
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
                    Users = 1
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

            var act = logic.OldesOrder().ToList();

             List<Order> exp = new List<Order> { new Order { OrderId = 4, UserId = 1, ProductId = 1, OrderDate = DateTime.Parse("2023.03.01") } };

            
            Assert.AreEqual(act, exp);



        }
        [Test]
        public void BuyerNutsTest()
        {

            var act = logic.BuyersOfNuts().First();

           int exp = 2;


            Assert.AreEqual(act, exp);



        }
        [Test]
        public void MostBuysTest()
        {

            var act = logic.MostBuys().FirstOrDefault();

            int exp = 4;


            Assert.AreEqual(act, exp);



        }
        [Test]
        public void MostSellsTest()
        {

            var act = logic.MostSells().FirstOrDefault();

            int exp = 3;


            Assert.AreEqual(act, exp);



        }

    }
}

