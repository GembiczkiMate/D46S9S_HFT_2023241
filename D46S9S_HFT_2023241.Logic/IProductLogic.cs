using D46S9S_HFT_2023241.Models;
using System.Linq;
using System.Reflection;

namespace D46S9S_HFT_2023241.Logic
{
    public interface IProductLogic
    {
        void Create(Product ord);
        void Delete(int id);
        Product Read(int id);
        IQueryable<Product> ReadAll();
        void Update(Product ord);
    }
}