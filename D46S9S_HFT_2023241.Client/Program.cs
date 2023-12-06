using ConsoleTools;
using D46S9S_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;

namespace D46S9S_HFT_2023241.Client
{
    internal class Program
    {
        static RestService rest;
        


        static void Create(string entity)
        {
            if (entity == "User")
            {
                Console.Write("Enter User Name: ");
                string name = Console.ReadLine();
                rest.Post(new User() { Username = name }, "User");
            }
            if (entity == "Order")
            {
                Console.Write("Enter the Id of the Product(1-5): ");
                int prod = int.Parse(Console.ReadLine());
                Console.Write("Enter the Id of the Product(1-5): ");
                int user = int.Parse( Console.ReadLine());
                int ordid = rest.Get<Order>("Order").Last().OrderId + 1;

                rest.Post(new Order() { OrderId = ordid, ProductId = prod ,UserId = user, OrderDate = DateTime.Now}, "Order");
            }
            if (entity == "Product")
            {
                Console.Write("Enter Product Name: ");
                string name = Console.ReadLine();
                Console.Write("Enter Product Price (numbers only): ");
                int price = int.Parse(Console.ReadLine());
                rest.Post(new Product() { ProductName = name,Price = price  }, "Product");
            }

        }
        static void List(string entity)
        {
            if (entity == "User")
            {
                List<User> users = rest.Get<User>("User");
                foreach (var item in users)
                {
                    Console.WriteLine(item.UserId+ ": "+ item.Username);
                }
            }
            if (entity == "Order")
            {
                List<Order> orders = rest.Get<Order>("Order");
                foreach (var item in orders)
                {
                    Console.WriteLine(item.OrderId + ": " + item.ProductId+", "+ item.UserId+", "+item.OrderDate.ToShortDateString());
                }
            }
            if (entity == "Product")
            {
                List<Product> products = rest.Get<Product>("Product");
                foreach (var item in products )
                {
                    Console.WriteLine(item.ProductId + ": " + item.ProductName +", "+item.Price);
                }
            }
           

            Console.ReadLine();
        }
        static void Update(string entity)
        {
            if (entity == "User")
            {
                Console.Write("Enter User's id to update: ");
                int id = int.Parse(Console.ReadLine());
                User one = rest.Get<User>(id, "User");
                Console.Write($"New name [old: {one.Username}]: ");
                string name = Console.ReadLine();
                one.Username = name;
                rest.Put(one, "User");
            }
            if (entity == "Order")
            {
                Console.Write("Enter Order's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Order one = rest.Get<Order>(id, "Order");
                Console.Write($"New UserID [old: {one.UserId}]: ");                
                int user = int.Parse(Console.ReadLine());
                Console.Write($"New ProductID [old: {one.ProductId}]: ");
                int prod = int.Parse(Console.ReadLine());
                one.UserId = user;
                one.ProductId = prod;
                one.OrderDate = DateTime.Now;
                one.OrderId = id;
                rest.Put(one, "Order");
            }
            if (entity == "Product")
            {
                Console.Write("Enter Product's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Product one = rest.Get<Product>(id, "Product");
                Console.Write($"New name [old: {one.ProductName}]: ");
                string name = Console.ReadLine();
                one.ProductName = name;
                rest.Put(one, "Product");
            }

        }
        static void Delete(string entity)
        {
            if (entity == "User")
            {
                Console.Write("Enter User's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "User");
            }
            if (entity == "Order")
            {
                Console.Write("Enter Order's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "Order");
            }
            if (entity == "Product")
            {
                Console.Write("Enter Product's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "Product");
            }

        }
        static void GetOldestOrder()
        {

            var result = rest.GetSingle<dynamic>("/NonCrud/GetOldestOrder");
            
           
           
                Console.WriteLine(result);
            
           
               
            

            Console.ReadLine();
         

        }
        static void GetDatas()
        {
            var result = rest.Get<dynamic>("/NonCrud/GetDatas");
            
            for (int i = 0; i < result.Count(); i++)
            {
                Console.WriteLine(result[i]);
            }
            
            Console.ReadLine();  
            
            


        }
        static void GetUsersOrder()
        {
            var result = rest.Get<dynamic>("/NonCrud/GetUsersOrder");

            for (int i = 0; i < result.Count(); i++)
            {
                Console.WriteLine(result[i]);
            }

            Console.ReadLine();




        }
        static void GetBuyersOfNuts()
        {
            var result = rest.Get<int>("/NonCrud/GetBuyersOfNutsID");
            
            List<User> users =new List<User>();
            for (int i = 0; i < result.Count; i++)
            {
                            
                users.Add(rest.Get<User>(result[i],"User"));
            }
            
            for (int i = 0; i < result.Count(); i++)
            {


                Console.WriteLine(users[i].UserId +": "+ users[i].Username);
            }

            Console.ReadLine();




        }
        static void GetMostBusys()
        {
            var result = rest.Get<User>("/NonCrud/GetMostBuysID");


            Console.WriteLine(result[0].UserId + ": " + result[0].Username);

            
            Console.ReadLine();




        }

        static void GetMostSells()
        {
            
            var result = rest.Get<Product>("/NonCrud/GetMostSellsID");

            Console.WriteLine(result[0].ProductId+": "+ result[0].ProductName+"-"+result[0].Price+" Ft");

            Console.ReadLine();


        }
        static void Main(string[] args)
        {

            rest = new RestService("http://localhost:39354/", "swagger");
            

            var userSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("User"))
                .Add("Create", () => Create("User"))
                .Add("Delete", () => Delete("User"))
                .Add("Update", () => Update("User"))
                .Add("Exit", ConsoleMenu.Close);

            var productSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Product"))
                .Add("Create", () => Create("Product"))
                .Add("Delete", () => Delete("Product"))
                .Add("Update", () => Update("Product"))
                .Add("Exit", ConsoleMenu.Close);




            var orderSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Order"))
                .Add("Create", () => Create("Order"))
                .Add("Delete", () => Delete("Order"))
                .Add("Update", () => Update("Order"))                
                .Add("Exit", ConsoleMenu.Close);

            var NonCrudSubmenu = new ConsoleMenu(args,level:1)
                .Add("GetDatas", () => GetDatas())
                .Add("GetUsers", () => GetUsersOrder())
                .Add("GetOldestOrder", () => GetOldestOrder())
                .Add("GetMostBuys", () => GetMostBusys())
                .Add("GetMostSells", () => GetMostSells())
                .Add("GetBuyersOfNuts", () => GetBuyersOfNuts())
                .Add("Exit", ConsoleMenu.Close);


            var menu = new ConsoleMenu(args, level: 0)
                .Add("Order", () => orderSubMenu.Show())
                .Add("User", () => userSubMenu.Show())
                .Add("Product", () => productSubMenu.Show())
                .Add("NonCrud",()=> NonCrudSubmenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();









        }
    }
}
