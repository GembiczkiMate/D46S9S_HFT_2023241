using D46S9S_HFT_2023241.Logic;
using D46S9S_HFT_2023241.Models;
using D46S9S_HFT_2023241.Repository;
using System;

namespace D46S9S_HFT_2023241.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cont = new OrderDB();
            var repo = new OrderRepository(cont);
            var logic = new OrderLogic(repo);

            
            var q = logic.AvgPrice();
           
            Order a = new Order
            {
                OrderId = 3,
                ProductId = 1,
                UserId = 4,
                OrderDate= DateTime.Parse("2023.11.02"),


            };
            Order b = new Order
            {
                OrderId = 4,
                ProductId = 1,
                UserId = 4,
                OrderDate = DateTime.Parse("2023.11.02"),


            };
            logic.Create(a);
            logic.Create(b);
            var items = logic.ReadAll();
            var g = logic.Datas();

            ;
        }
    }
}
