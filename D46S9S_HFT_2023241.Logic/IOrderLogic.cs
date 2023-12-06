using D46S9S_HFT_2023241.Models;

using System.Collections.Generic;
using System.Linq;
using static D46S9S_HFT_2023241.Logic.OrderLogic;

namespace D46S9S_HFT_2023241.Logic
{
    public interface IOrderLogic
    {
        
        void Create(Order ord);
        
        void Delete(int id);
        Order Read(int id);
        IQueryable<Order> ReadAll();
        void Update(Order ord);

        IEnumerable<Order> OldesOrder();
        IEnumerable<int> BuyersOfNutsID();
        IEnumerable<Data> Datas();
        IEnumerable<User> MostBuysID();
        IEnumerable<Product> MostSellsID();
        IEnumerable<UO> UsersOrder();
    }
}