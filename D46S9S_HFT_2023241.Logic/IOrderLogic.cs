using D46S9S_HFT_2023241.Models;
using System.Linq;

namespace D46S9S_HFT_2023241.Logic
{
    public interface IOrderLogic
    {
        
        void Create(Order ord);
        
        void Delete(int id);
        Order Read(int id);
        IQueryable<Order> ReadAll();
        void Update(Order ord);
    }
}