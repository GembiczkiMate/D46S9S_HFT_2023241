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

            var items = logic.ReadAll();
            var q = logic.AvgPrice();
            ;
        }
    }
}
