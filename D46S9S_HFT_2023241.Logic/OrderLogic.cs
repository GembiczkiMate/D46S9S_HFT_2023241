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
            return (from x in this.rep.ReadAll()
                   orderby x.ProductId
                   group x by x.ProductId into g                   
                   select new Data
                   {

                       Id =g.Key,
                       Users = g.Count(),


                   }).ToList();

        }

        public class Data
        {

            public int Users { get; set; }
            public int Id { get; set; }
            public override bool Equals(object obj)
            {
               Data other = obj as Data;
                if (other == null)
                {
                    return false;
                }                  
                else
                {
                    return this.Id == other.Id
                    && this.Users == other.Users;
                }
            }
            public override int GetHashCode()
            {
                return HashCode.Combine(this.Id, this.Users);
            }

        }

        public IEnumerable<Order> OldesOrder() 
        {
            return (from x in rep.ReadAll()                    
                    orderby x.OrderDate
                    select x).Take(1);
        }

        public IEnumerable<User> BuyersOfNuts()
        {

            return (from x in rep.ReadAll()
                   where x.ProductId == 5
                   group x by x.User into g
                   select g.Key).Take(1);
                   
                   
                   



        }

        public IEnumerable<User> MostBuys()
        {
            return ((from x in rep.ReadAll()
                   group x by x.UserId into g
                   orderby g.Count() descending
                   select g.FirstOrDefault().User).Take(1));
                   
                      
        }


        public IEnumerable<Product> MostSells()
        {

            return (from x in rep.ReadAll()                   
                   group x by x.Products into g
                   orderby g.Count() descending
                   select g.First().Products).Take(1).AsEnumerable();
        }




    }
}
