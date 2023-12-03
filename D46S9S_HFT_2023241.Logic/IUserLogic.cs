using D46S9S_HFT_2023241.Models;
using System.Linq;

namespace D46S9S_HFT_2023241.Logic
{
    public interface IUserLogic
    {
        void Create(User ord);
        void Delete(int id);
        User Read(int id);
        IQueryable<User> ReadAll();
        void Update(User ord);
    }
}