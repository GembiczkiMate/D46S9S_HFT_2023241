using ConsoleTools;
using D46S9S_HFT_2023241.Logic;
using D46S9S_HFT_2023241.Models;
using D46S9S_HFT_2023241.Repository;
using System;
using System.Collections.Generic;

namespace D46S9S_HFT_2023241.Client
{
    internal class Program
    {

        static OrderLogic orderLogic;
        static ProductLogic productLogic;
        static UserLogic userLogic;


        static void Create(string entity)
        {
            Console.WriteLine(entity + " create");
            Console.ReadLine();
        }
        static void List(string entity)
        {
            if (entity == "User")
            {
                var items = userLogic.ReadAll();
                Console.WriteLine("Id" + "\t" + "Name");
                foreach (var item in items)
                {
                    Console.WriteLine(item.UserId + "\t" + item.Username);
                }
            }
            Console.ReadLine();
        }
        static void Update(string entity)
        {
            Console.WriteLine(entity + " update");
            Console.ReadLine();
        }
        static void Delete(string entity)
        {
            Console.WriteLine(entity + " delete");
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            var ctx = new OrderDB();

            var movieRepo = new OrderRepository(ctx);
            var roleRepo = new UserRepository(ctx);
            var actorRepo = new ProductRepository(ctx);


            orderLogic = new OrderLogic(movieRepo);
            userLogic = new UserLogic(roleRepo);
            productLogic = new ProductLogic(actorRepo);

            var userSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Actor"))
                .Add("Create", () => Create("Actor"))
                .Add("Delete", () => Delete("Actor"))
                .Add("Update", () => Update("Actor"))
                .Add("Exit", ConsoleMenu.Close);

            var productSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Role"))
                .Add("Create", () => Create("Role"))
                .Add("Delete", () => Delete("Role"))
                .Add("Update", () => Update("Role"))
                .Add("Exit", ConsoleMenu.Close);



            var orderSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Order"))
                .Add("Create", () => Create("Order"))
                .Add("Delete", () => Delete("Order"))
                .Add("Update", () => Update("Order"))
                .Add("Exit", ConsoleMenu.Close);


            var menu = new ConsoleMenu(args, level: 0)
                .Add("Order", () => orderSubMenu.Show())
                .Add("User", () => userSubMenu.Show())
                .Add("Product", () => productSubMenu.Show())

                .Add("Exit", ConsoleMenu.Close);

            menu.Show();









        }
    }
}
