using System;
using D46S9S_HFT_2023241.Repository;
using D46S9S_HFT_2023241.Models;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection.Metadata;
using System.Collections.Generic;

namespace D46S9S_HFT_2023241.Logic
{
    public class OrderLogic : IOrderLogic
    {

        IRepository<Order> rep;

        public OrderLogic(IRepository<Order> rep)
        {


            this.rep = rep;
        }
        public void Create(Order ord)
        {

            if (ord.OrderDate > DateTime.Now)
            {
                throw new ArgumentException("Not possible");
            }
            this.rep.Create(ord);

        }
        public void Delete(int id)
        {
            this.rep.Delete(id);

        }
        public Order Read(int id)
        {
           return this.rep.Read(id);

        }
        public IQueryable<Order> ReadAll()
        {
            return this.rep.ReadAll();


        }
        public void Update(Order ord)
        {
            this.rep.Update(ord);

        }


        

        public IEnumerable<Data> Datas()
        {
            return from x in this.rep.ReadAll()
                   group x by x.User.Username into g
                   select new Data
                   {

                       Name =g.Key,
                       Users = g.Count(),


                   };

        }

        public class Data
        {

            public int Users { get; set; }
            public string Name { get; set; }

        }

        public IEnumerable<Order> OldesOrder() 
        {
            return from x in rep.ReadAll()
                   group x by x.OrderDate into g
                   select g.First();
        }

        public IEnumerable<List<Order>> BuyersOfNuts()
        {

            return from x in rep.ReadAll()
                   where x.Products.ProductName == "nuts"
                   group x by x.User.Username into g
                   select g.ToList();
                   

        }

        public IEnumerable<Order> MostBuys()
        {
            return from x in rep.ReadAll()
                   group x by x.User into g
                   select (Order)g.First();            
        }


        public IEnumerable<Order> MostSells()
        {

            return from x in rep.ReadAll()
                   group x by x.ProductId into g
                   select (Order)g.First();
        }




    }
}
