using ConsoleTools;
using D46S9S_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
            Console.ReadLine();
        }
        static void Update(string entity)
        {
            if (entity == "User")
            {
                Console.Write("Enter Actor's id to update: ");
                int id = int.Parse(Console.ReadLine());
                User one = rest.Get<User>(id, "User");
                Console.Write($"New name [old: {one.Username}]: ");
                string name = Console.ReadLine();
                one.Username = name;
                rest.Put(one, "User");
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


            var menu = new ConsoleMenu(args, level: 0)
                .Add("Order", () => orderSubMenu.Show())
                .Add("User", () => userSubMenu.Show())
                .Add("Product", () => productSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();









        }
    }
}
