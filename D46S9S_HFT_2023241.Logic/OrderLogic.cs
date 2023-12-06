using System;
using D46S9S_HFT_2023241.Repository;
using D46S9S_HFT_2023241.Models;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection.Metadata;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using static D46S9S_HFT_2023241.Logic.OrderLogic;

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

        public IEnumerable<Data> Datas()
        {
            return (from x in this.rep.ReadAll()
                   orderby x.Products.ProductName
                   group x by x.ProductId into g                   
                   select new Data
                   {
                       Id =g.Key,
                       Users = g.Count(),
                   }).ToList();
        }

        public IEnumerable<UO> UsersOrder()
        {
            return (from x in this.rep.ReadAll()                                       
                    select new UO
                    {
                        UserId = x.User.UserId,
                        Username= x.User.Username,
                        Prod = x.Products.ProductName

                    }).ToList();
        }
        public class UO
        {
            public int UserId { get; set; }
            public string Username { get; set; }
            public string Prod { get; set; }

            public override bool Equals(object obj)
            {
                UO other = obj as UO;
                if (other == null)
                {
                    return false;
                }
                else
                {
                    return this.UserId == other.UserId
                        && this.Username == other.Username
                        && this.Prod == other.Prod;
                  
                }
            }
            public override int GetHashCode()
            {
                return HashCode.Combine(this.UserId, this.Username,this.Prod);
            }
        }



        public IEnumerable<Order> OldesOrder() 
        {
            var s = ((from y in rep.ReadAll()
                      orderby y.OrderDate
                      select y.OrderDate).FirstOrDefault());

            return (from x in this.rep.ReadAll()
                    where x.OrderDate == (((from y in rep.ReadAll()
                                           orderby y.OrderDate
                                           select y.OrderDate).ToList()).AsEnumerable().FirstOrDefault())
                                          
                    select new Order
                    {
                        
                        ProductId=x.ProductId,
                        OrderDate =x.OrderDate,
                        OrderId =x.OrderId,
                        UserId=x.UserId,
                        
                        



                    }).AsEnumerable().ToList();
        }
        public class Oldest
        {
            public DateTime Date { get; set; }
            public string User { get; set; }
            public string Product { get; set; }
            public int Price { get; set; }
            public override bool Equals(object obj)
            {
                Oldest other = obj as Oldest;
                if (other == null)
                {
                    return false;
                }
                else
                {
                    return this.User == other.User
                        && this.Product == other.Product
                        && this.Price == other.Price
                        && this.Date == other.Date;

                }
            }
            public override int GetHashCode()
            {
                return HashCode.Combine(this.User, this.Product, this.Price, this.Date);
            }


        }

        public IEnumerable<int> BuyersOfNutsID()
        {              
            return (from x in this.rep.ReadAll()
                    where x.Products.ProductId ==5
                    orderby x.ProductId
                    group x by x.UserId into g
                    select g.Key).ToList();
        }
        

        public IEnumerable<User> MostBuysID()
        {
            



            return (from x in this.rep.ReadAll()
                    where x.UserId == ((from y in this.rep.ReadAll()
                                        group y by y.UserId into g
                                        orderby g.Count() descending
                                        select g.Key).FirstOrDefault())
            select x.User).Distinct().AsEnumerable();
        }


        public IEnumerable<Product> MostSellsID()
        {
                  



            return (from x in this.rep.ReadAll()
                    where x.ProductId == ((from y in this.rep.ReadAll()
                                           group y by y.ProductId into g
                                           orderby g.Count() descending
                                           select g.Key).FirstOrDefault())
                    select x.Products).Distinct().AsEnumerable();
        }




    }

   
}
