using D46S9S_HFT_2023241.Models;
using System.Linq;

namespace D46S9S_HFT_2023241.Logic
{
    public interface IOrderLogic
    {
        double? AvgPrice();
        void Create(Order ord);
        IQueryable<OrderLogic.Data> Datas();
        void Delete(int id);
        void Read(int id);
        IQueryable<Order> ReadAll();
        void Update(Order ord);
    }
}