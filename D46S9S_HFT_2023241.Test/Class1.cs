using D46S9S_HFT_2023241.Repository;
using D46S9S_HFT_2023241.Models;
using System;
using System.Linq;

namespace D46S9S_HFT_2023241.Test
{
    public class Class1
    {
        static void Main(string[] args)
        {
            OrderDB a = new OrderDB();

            var asd = a.users.ToArray();

            foreach (var item in asd)
            {

                Console.WriteLine(item.Username);
            }
        }

    }
}

