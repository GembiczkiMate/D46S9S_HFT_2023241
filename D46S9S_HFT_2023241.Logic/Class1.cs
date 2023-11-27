using System;
using D46S9S_HFT_2023241.Repository;
using D46S9S_HFT_2023241.Models;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection.Metadata;

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
        public void Read(int id)
        {
            this.rep.Read(id);

        }
        public IQueryable<Order> ReadAll()
        {
            return this.rep.ReadAll();


        }
        public void Update(Order ord)
        {
            this.rep.Update(ord);

        }


        public double? AvgPrice() => this.rep.ReadAll().Average(t => t.Products.Price);




        public IQueryable<Data> Datas()
        {
            return from x in this.rep.ReadAll()
                   group x by x.Products.ProductName into g
                   select new Data
                   {

                       ID =( g.Sum(t => t.ProductId)) / g.Count(),
                       Users = g.Count(),


                   };

        }

        public class Data
        {

            public int Users { get; set; }
            public int ID { get; set; }

        }




    }
}
