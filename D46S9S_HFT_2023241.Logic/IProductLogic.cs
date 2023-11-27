using D46S9S_HFT_2023241.Models;
using System.Linq;

namespace D46S9S_HFT_2023241.Logic
{
    internal interface IProductLogic
    {
        void Create(Product ord);
        void Delete(int id);
        void Read(int id);
        IQueryable<Product> ReadAll();
        void Update(Product ord);
    }
}